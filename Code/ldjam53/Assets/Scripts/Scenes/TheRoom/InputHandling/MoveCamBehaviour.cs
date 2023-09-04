using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class MoveCamBehaviour : ManipulationInterface
    {

        //public Camera cam;


        private readonly float moveSpeedTouch = 0.085f;
        private readonly float moveSpeedMouseDrag = 1.5f;
        private readonly float zoomSpeedMouse = 20.0f;
        private readonly float zoomSpeedTouch = 0.25f;


        private (Vector2, Vector2) prevPinch = default;
        private Vector3 clickDown;


        public static int panTimeout { get; private set; } = 0;
        private static bool isMoving { get; set; } = false;



        void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HorizontalMovment();
                VerticalMovment();
            }
        }

        public static bool IsPanning()
        {
            return panTimeout >= 1;
        }


        private void HorizontalMovment()
        {
            float moveX = 0;
            float moveZ = 0;

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
                inputHandler.cam.transform.position += ApplyCamCorrection(moveX, 0, moveZ) * Time.deltaTime * Base.Core.Game.Options.MoveSensivity;
            }
        }


        private float GetCamCorrectionAngle()
        {
            float camAngle = inputHandler.cam.transform.eulerAngles.y;
            return Mathf.Deg2Rad * camAngle;
        }
        private Vector3 ApplyCamCorrection(float inputX, float inputY, float inputZ)
        {
            var radCorrectionAngle = GetCamCorrectionAngle();
            var xPartX = Mathf.Sin(radCorrectionAngle) * inputZ;
            var xPartZ = Mathf.Cos(radCorrectionAngle) * inputZ;
            var zPartX = Mathf.Cos(-radCorrectionAngle) * inputX;
            var zPartZ = Mathf.Sin(-radCorrectionAngle) * inputX;

            return new Vector3(xPartX + zPartX, inputY, xPartZ + zPartZ);

        }


        private void VerticalMovment()
        {
            float vertical = 0.0f;
            if (Input.mouseScrollDelta.y != 0)
            {
                vertical = zoomSpeedMouse * Input.mouseScrollDelta.y;
            }
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
                    vertical = zoomSpeedTouch * (Vector2.Distance(touch1.position, touch2.position) - Vector2.Distance(prevPinch.Item1, prevPinch.Item2));
                    prevPinch = (touch1.position, touch2.position);
                    panTimeout = 20;

                }
            }
            if (vertical != 0)
            {
                vertical *= Time.deltaTime * Base.Core.Game.Options.ZoomSensivity;
                Vector3 newCamPos = inputHandler.cam.transform.position + new Vector3(0, vertical, 0);

                inputHandler.cam.transform.position = newCamPos;
            }
        }
        public override void OnButtonBottomMiddle()
        {
            inputHandler.CamMoveBack();
        }

        public override void OnButtonMidleLeft()
        {
            inputHandler.CamMoveLeft();
        }

        public override void OnButtonMiddleRight()
        {
            inputHandler.CamMoveRight();
        }

        public override void OnButtonTopLeft()
        {
            inputHandler.CamMoveDown();
        }

        public override void OnButtonTopMiddle()
        {
            inputHandler.CamMoveForeward();
        }

        public override void OnButtonTopRight()
        {
            inputHandler.CamMoveUp();
        }
    }


}