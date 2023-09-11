using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class RotateSelectedBehaviour :  ManipulationInterface
    {
        //private Camera cam;
        //private TheRoomBehaviour theRoomBehaviour;

        //public InputHandler inputHandler;

        private float threshhold = 5f;

        private float moveInterval = 0.1f;
        private float currentInterval = 0f;


        private (Vector2, Vector2) prevPinch = default;
        private Vector3 clickDown;


        private static bool isMoving { get; set; } = false;

        //private void Awake()
        //{
        //    cam = inputHander.cam;
        //    theRoomBehaviour = inputHander.theRoomBehaviour;
        //}

        void Update()
        {

            var selected = inputHandler.theRoomBehaviour.selectedElement;
            if (selected != default && !EventSystem.current.IsPointerOverGameObject())
            {
                HorizontalMovment(selected);
                VerticalMovment(selected);
            }
        }


        private void HorizontalMovment(RoomElementBehaviour selected)
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
                    moveX = CheckThreshhold(moveX);
                    moveZ = CheckThreshhold(moveZ);
                    clickDown = Input.mousePosition;
                }
            }
            else if (isMoving && Input.GetMouseButtonUp(0))
            {
                isMoving = false;
            }
            if (currentInterval <= 0 && (moveX != 0 || moveZ != 0))
            {
                if (moveX > 0)
                {
                    moveX = 90;
                }
                else if (moveX < 0)
                {
                    moveX = -90;
                }
                if (moveZ > 0)
                {
                    moveZ = -90;
                }
                else if (moveZ < 0)
                {
                    moveZ = 90;
                }
                selected.transform.Rotate(GetRotationVector(moveX, moveZ), Space.World);
                currentInterval = moveInterval;
                selected.UpdateIsPlaceable();
            }
            else
            {
                currentInterval -= Time.deltaTime;
            }
        }

        private Vector3 GetRotationVector(float moveX, float moveZ)
        {
            float camAngle = inputHandler.cam.transform.eulerAngles.y;
            if (camAngle >= 45 && camAngle < 135)
            {
                return new Vector3(0, moveX, moveZ);
            }
            else if (camAngle >= 135 && camAngle < 225)
            {
                return new Vector3(moveZ, moveX, 0);
            }
            else if (camAngle >= 225 && camAngle < 312)
            {
                return new Vector3(0, moveX, moveZ);
            }
            return new Vector3(moveZ, moveX, 0);

        }



        private float CheckThreshhold(float moveX)
        {
            if (-threshhold < moveX && moveX < threshhold)
            {
                moveX = 0;
            }

            return moveX;
        }

        private void VerticalMovment(RoomElementBehaviour selected)
        {
            float vertical = 0.0f;
            if (Input.mouseScrollDelta.y != 0)
            {
                vertical = Input.mouseScrollDelta.y;
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
                    vertical = (Vector2.Distance(touch1.position, touch2.position) - Vector2.Distance(prevPinch.Item1, prevPinch.Item2));
                    prevPinch = (touch1.position, touch2.position);
                }
            }

            if (vertical != 0)
            {
                if (vertical > 0)
                {
                    vertical = 90;
                } else
                {
                    vertical = -90;
                }
                Vector3 rotationVector;
                float camAngle = inputHandler.cam.transform.eulerAngles.y;
                if (camAngle >= 45 && camAngle < 135)
                {
                    rotationVector = new Vector3(vertical, 0, 0);
                }
                else if (camAngle >= 135 && camAngle < 225)
                {
                    rotationVector = new Vector3(0, 0, vertical);
                }
                else if (camAngle >= 225 && camAngle < 312)
                {
                    rotationVector = new Vector3(vertical, 0, 0);
                }
                else
                {
                    rotationVector = new Vector3(0, 0, vertical);
                }
                selected.transform.Rotate(rotationVector, Space.World);
                selected.UpdateIsPlaceable();
            }
        }

        public override void OnButtonBottomMiddle()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {
                inputHandler.RotateXN();
            }
        }

        public override void OnButtonMidleLeft()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {
                inputHandler.RotateYN();
            }
        }

        public override void OnButtonMiddleRight()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {
                inputHandler.RotateYP();
            }
        }

        public override void OnButtonTopLeft()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {
                inputHandler.RotateZN();
            }
        }

        public override void OnButtonTopMiddle()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {
                inputHandler.RotateXP();
            }
        }

        public override void OnButtonTopRight()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {
                inputHandler.RotateZP();
            }
        }
    }
}