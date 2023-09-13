using System;

using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class MoveSelectedBehaviour : ManipulationInterface
    {
        //public InputHandler inputHandler;

        private float threshhold = 0.1f;

        private float moveInterval = 0.1f;
        private float currentInterval = 0f;


        private (Vector2, Vector2) prevPinch = default;
        private Vector3 clickDown;


        private static bool isMoving { get; set; } = false;



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
                    moveX = -1;
                }
                else
                {
                    moveX = 1;
                }
                if (moveZ > 0)
                {
                    moveZ = -1;
                }
                else
                {
                    moveZ = 1;
                }
                ApplyHorizontalMovment(selected, moveX, moveZ);
            }
            else
            {
                currentInterval -= Time.deltaTime;
            }
        }



        private float CheckThreshhold(float moveX)
        {
            if (-threshhold < moveX && moveX < threshhold)
            {
                moveX = 0;
            }

            return moveX;
        }


        private void ApplyHorizontalMovment(RoomElementBehaviour selected, Vector3 vector)
        {
            selected.transform.position += vector;
            currentInterval = moveInterval;
            selected.UpdateIsPlaceable();
        }


        private void ApplyHorizontalMovment(RoomElementBehaviour selected, float moveX, float moveZ)
        {
            var correctedVector = ApplyCamCorrection(moveX, 0, moveZ);
            moveX = Mathf.RoundToInt(correctedVector.x);
            moveZ = Mathf.RoundToInt(correctedVector.z);
            ApplyHorizontalMovment(selected, new Vector3(moveX, 0, moveZ));
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


            var outX = (xPartX + zPartX) * .9f;
            var outZ = (xPartZ + zPartZ) * .9f;
            var vec = new Vector3(outX, inputY, outZ);
            return vec;

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

            vertical = CheckThreshhold(vertical);
            if (vertical != 0)
            {

                if (vertical > 0)
                {
                    vertical = 1;
                }
                else
                {
                    vertical = -1;
                }
                MoveVertical(selected, vertical);
            }
        }

        private static void MoveVertical(RoomElementBehaviour selected, Single vertical)
        {
            selected.transform.position += new Vector3(0, vertical, 0);
            selected.UpdateIsPlaceable();
        }


        public override void OnButtonBottomMiddle()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {
                inputHandler.MoveBack();
            }
        }

        public override void OnButtonMidleLeft()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {
                inputHandler.MoveLeft();
            }
        }

        public override void OnButtonMiddleRight()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {

                inputHandler.MoveRight();
            }
        }

        public override void OnButtonTopLeft()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {

                inputHandler.MoveDown();
            }
        }

        public override void OnButtonTopMiddle()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {

                inputHandler.MoveForeward();
            }
        }

        public override void OnButtonTopRight()
        {
            if (inputHandler.theRoomBehaviour.selectedElement != default)
            {

                inputHandler.MoveUp();
            }
        }
    }
}