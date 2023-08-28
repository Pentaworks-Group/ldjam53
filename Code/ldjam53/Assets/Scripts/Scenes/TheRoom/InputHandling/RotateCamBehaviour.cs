using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class RotateCamBehaviour : ManipulationInterface
    {
        public InputHandler inputHandler;
        //public Camera cam;


        private readonly float moveSpeedTouch = 0.085f;
        private readonly float moveSpeedMouseDrag = 1.5f;
        private readonly float zoomSpeedMouse = 20.0f;
        private readonly float zoomSpeedTouch = 0.25f;


        private (Vector2, Vector2) prevPinch = default;
        private Vector3 clickDown;


        public static int panTimeout { get; private set; } = 0;
        private static bool isLooking { get; set; } = false;



        void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                LookHandling();
                VerticalMovment();
            }
        }

        public static bool IsPanning()
        {
            return panTimeout >= 1;
        }


        private void LookHandling()
        {

            float lookX = 0;
            float lookY = 0;


            if (Input.GetMouseButtonDown(0))
            {
                clickDown = Input.mousePosition;
                isLooking = true;
            }
            else if (Input.touchCount == 2)
            {
                isLooking = false;
            }
            else if (isLooking && Input.GetMouseButton(0))
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
            else if (isLooking && Input.GetMouseButtonUp(0))
            {
                isLooking = false;
            }

            if (lookX != 0 || lookY != 0)
            {
                inputHandler.cam.transform.Rotate(new Vector3(-lookY, lookX, 0) * Time.deltaTime * Base.Core.Game.Options.LookSensivity);
                float z = inputHandler.cam.transform.eulerAngles.z;
                inputHandler.cam.transform.Rotate(0, 0, -z);
            }
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
                Vector3 newCamPos = inputHandler.cam.transform.position + inputHandler.cam.transform.forward * vertical;
                inputHandler.cam.transform.position = newCamPos;
            }
        }


        public override void OnButtonBottomMiddle()
        {
            inputHandler.RotateXN();
        }

        public override void OnButtonMidleLeft()
        {
            inputHandler.RotateYN();
        }

        public override void OnButtonMiddleRight()
        {
            inputHandler.RotateYP();
        }

        public override void OnButtonTopLeft()
        {
            inputHandler.RotateZN();
        }

        public override void OnButtonTopMiddle()
        {
            inputHandler.RotateXP();
        }

        public override void OnButtonTopRight()
        {
            inputHandler.RotateZP();
        }
    }
}