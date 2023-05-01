using Assets.Scripts.Base;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class MoveSelectedBehaviour : MonoBehaviour
    {
        public Camera cam;
        public TheRoomBehaviour TheRoomBehaviour;


        private float threshhold = 0.1f;

        private float moveInterval = 0.1f;
        private float currentInterval = 0f;


        private (Vector2, Vector2) prevPinch = default;
        private Vector3 clickDown;


        private static bool isMoving { get; set; } = false;



        void Update()
        {

            var selected = TheRoomBehaviour.selectedElement;
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
                var correctedVector = ApplyCamCorrection(moveX, 0, moveZ);
                correctedVector = new Vector3(Mathf.RoundToInt(correctedVector.x), 0, Mathf.RoundToInt(correctedVector.z));
                selected.transform.position += correctedVector;
                currentInterval = moveInterval;
                selected.UpdateIsPlaceable();
            } else
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

        private float GetCamCorrectionAngle()
        {
            float camAngle = cam.transform.eulerAngles.y;
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
                    vertical = 1;
                }
                else
                {
                    vertical = -1;
                }
                selected.transform.position += new Vector3(0, vertical, 0);
                selected.UpdateIsPlaceable();
            }
        }
    }
}