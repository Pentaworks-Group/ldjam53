using Assets.Scripts.Base;

using UnityEngine;

namespace Assets.Scripts.Scenes.TheRoom
{


    public class CameraBehaviour : MonoBehaviour
    {
        public Camera cam;


        private readonly float moveSpeed = 10f;
        private readonly float moveSpeedTouch = 0.085f;
        private readonly float moveSpeedMouseDrag = 1f;
        private readonly float zoomSpeed = 10.0f;
        private readonly float zoomSpeedMouse = 20.0f;
        private readonly float zoomSpeedTouch = 0.25f;


        private (Vector2, Vector2) prevPinch = default;
        private Vector3 clickDown;


        public static int panTimeout { get; private set; } = 0;
        private static bool isMoving { get; set; } = false;
        private static bool isLooking { get; set; } = false;



        // Update is called once per frame
        void Update()
        {
            //if (!Core.Game.LockCameraMovement)
            //{
            //    if (!isMoving && panTimeout > 0)
            //    {
            //        panTimeout -= 1;
            //    }

            bool zoomChanged = ZoomHandling();
            bool positionChanged = MoveHandling();
            //    if (zoomChanged || positionChanged)
            //    {
            //        UpdateFarmButton();
            //    }
            //}
            LookHandling();
        }

        public static bool IsPanning()
        {
            return panTimeout >= 1;
        }

        private void LookHandling()
        {

            float lookX = 0;
            float lookY = 0;


            if (Input.GetMouseButtonDown(1))
            {
                clickDown = Input.mousePosition;
                isLooking = true;
            }
            else if (Input.touchCount == 2)
            {
                isLooking = false;
            }
            else if (isLooking && Input.GetMouseButton(1))
            {
                if (Input.mousePosition != clickDown)
                {
                    lookX = (clickDown.x - Input.mousePosition.x);
                    lookY = (clickDown.y - Input.mousePosition.y);
                    if (Application.isMobilePlatform)
                    {
                        lookX *= moveSpeedTouch;
                        lookY *= moveSpeedTouch;
                    }
                    else
                    {
                        lookX *= moveSpeedMouseDrag;
                        lookY *= moveSpeedMouseDrag;
                    }
                    clickDown = Input.mousePosition;
                    panTimeout = 2;
                }
            }
            else if (isLooking && Input.GetMouseButtonUp(1))
            {
                isLooking = false;
            }

            if (lookX != 0  || lookY != 0)
            {
                cam.transform.Rotate(new Vector3(-lookY, lookX, 0) * Time.deltaTime * Base.Core.Game.Options.LookSensivity);
                float z = cam.transform.eulerAngles.z;
                cam.transform.Rotate(0, 0, -z);
            }
        }


        private bool MoveHandling()
        {
            //// Movement along x, z axis
            // Keys
            float moveX = 0;

            float moveZ = 0;

            // Keys
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveX = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveX = +1;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveZ = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                moveZ = -1;
            }

            if (Base.Core.Game.Options.IsMouseScreenEdgeScrollingEnabled)
            {
                //Camera angle
                float angle = (float)(cam.transform.rotation.y);

                // Mouse
                if (Input.mousePosition.x <= Screen.width * 0.01f)
                {
                    moveX = 1.0f * Mathf.Sin(angle);
                }
                else if (Input.mousePosition.x >= Screen.width * 0.99f)
                {
                    moveX = -1.0f * Mathf.Sin(angle);
                }

                if (Input.mousePosition.y <= Screen.height * 0.01f)
                {
                    moveZ = -1.0f * Mathf.Cos(angle);
                }
                else if (Input.mousePosition.y >= Screen.height * 0.99f)
                {
                    moveZ = 1.0f * Mathf.Cos(angle);
                }
            }

            if (moveX != 0)
            {
                moveX *= moveSpeed;
            }

            if (moveZ != 0)
            {
                moveZ *= moveSpeed;
            }

            if (Input.GetMouseButtonDown(0))
            {
                clickDown = Input.mousePosition;
                isMoving = true;
            }
            else if (Input.touchCount == 2)
            {
                isMoving = false;
            }
            else if (isMoving && Input.GetMouseButton(0))
            {
                if (Input.mousePosition != clickDown)
                {
                    moveX = (clickDown.x - Input.mousePosition.x);
                    moveZ = (clickDown.y - Input.mousePosition.y);
                    if (Application.isMobilePlatform)
                    {
                        moveX *= moveSpeedTouch;
                        moveZ *= moveSpeedTouch;
                    }
                    else
                    {
                        moveX *= moveSpeedMouseDrag;
                        moveZ *= moveSpeedMouseDrag;
                    }
                    clickDown = Input.mousePosition;
                    panTimeout = 2;
                }
            }
            else if (isMoving && Input.GetMouseButtonUp(0))
            {
                isMoving = false;
            }

            if (moveX != 0 || moveZ != 0)
            {
                //var inputDir = new Vector3(moveX, 0, moveZ);
                //var moveDir = Vector3.Scale(inputDir, cam.transform.forward);
                var moveDir = new Vector3(moveX, 0, moveZ);
                //var moveDir = cam.ViewportToWorldPoint(inputDir);
                moveDir *= Time.deltaTime * Base.Core.Game.Options.MoveSensivity;
                //Debug.Log($"moveDir:{moveDir}; forward: {cam.transform.forward}; inputDir: {inputDir}");
                cam.transform.position += moveDir;
                return true;
            }

            return false;
        }

        private bool ZoomHandling()
        {
            //// Zoom
            float zoom = 0.0f;

            // Keys
            if (Input.GetKey(KeyCode.Q))
            {
                zoom = -zoomSpeed;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                zoom = zoomSpeed;
            }

            // Mouse
            if (Input.mouseScrollDelta.y != 0)
            {
                zoom = zoomSpeedMouse * Input.mouseScrollDelta.y;
            }

            // Touch
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                if (prevPinch == default)
                {
                    prevPinch = (touch1.position, touch2.position);
                }
                else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    zoom = zoomSpeedTouch * (Vector2.Distance(touch1.position, touch2.position) - Vector2.Distance(prevPinch.Item1, prevPinch.Item2));
                    prevPinch = (touch1.position, touch2.position);
                    panTimeout = 20;
                }
            }

            if (zoom != 0)
            {
                Vector3 newCamPos = cam.transform.position + cam.transform.forward * zoom * Time.deltaTime * Base.Core.Game.Options.ZoomSensivity;
                if (newCamPos.y < 1)
                {
                    return false;
                }

                cam.transform.position = newCamPos;
                return true;
            }

            return false;
        }

    }

}
