using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;



namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class InputHandler : MonoBehaviour
    {
        public Camera cam;

        public TheRoomBehaviour theRoomBehaviour;
        private float moveInterval = 0.1f;
        private float currentInterval = 0f;

        //public void Update()
        //{
        //    if (currentInterval <= 0)
        //    {
        //        if (theRoomBehaviour.selectedElement != default)
        //        {
        //            if (Input.GetKey(KeyCode.W))
        //            {
        //                MoveForeward();
        //            }
        //            else if (Input.GetKey(KeyCode.S))
        //            {
        //                MoveBack();
        //            }
        //            else if (Input.GetKey(KeyCode.A))
        //            {
        //                MoveLeft();
        //            }
        //            else if (Input.GetKey(KeyCode.D))
        //            {
        //                MoveRight();
        //            }
        //            else if (Input.GetKey(KeyCode.Q))
        //            {
        //                MoveUp();
        //            }
        //            else if (Input.GetKey(KeyCode.E))
        //            {
        //                MoveDown();
        //            }
        //            else if (Input.GetKey(KeyCode.Z))
        //            {
        //                RotateXP();
        //            }
        //            else if (Input.GetKey(KeyCode.X))
        //            {
        //                RotateZP();
        //            }
        //            else if (Input.GetKey(KeyCode.C))
        //            {
        //                RotateYP();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        currentInterval -= Time.deltaTime;
        //    }
        //}

        public void RotateYN()
        {
            Rotate(new Vector3(0, 0, -90));
        }
        public void RotateYP()
        {
            Rotate(new Vector3(0, 0, 90));
        }

        public void RotateZN()
        {
            Rotate(new Vector3(0, -90, 0));
        }

        public void RotateZP()
        {
            Rotate(new Vector3(0, 90, 0));
        }

        public void RotateXN()
        {
            Rotate(new Vector3(-90, 0, 0));
        }

        public void RotateXP()
        {
            Rotate(new Vector3(90, 0, 0));
        }

        public void MoveDown()
        {
            Move(new Vector3(0, -1, 0));
        }

        public void MoveUp()
        {
            Move(new Vector3(0, 1, 0));
        }

        public void MoveRight()
        {
            var radCorrectionAngle = GetCorrectionAngle();
            Move(new Vector3(Mathf.Cos(-radCorrectionAngle), 0, Mathf.Sin(-radCorrectionAngle)));
        }

        public void MoveLeft()
        {
            var radCorrectionAngle = GetCorrectionAngle();
            Move(new Vector3(-Mathf.Cos(-radCorrectionAngle), 0, -Mathf.Sin(-radCorrectionAngle)));
        }

        public void MoveBack()
        {
            var radCorrectionAngle = GetCorrectionAngle();
            Move(new Vector3(-Mathf.Sin(radCorrectionAngle), 0, -Mathf.Cos(radCorrectionAngle)));
        }

        public void MoveForeward()
        {
            var radCorrectionAngle = GetCorrectionAngle();
            Move(new Vector3(Mathf.Sin(radCorrectionAngle), 0, Mathf.Cos(radCorrectionAngle)));
        }

        private System.Single GetCorrectionAngle()
        {
            float camAngle = cam.transform.eulerAngles.y;
            float correctionAngle = 0;
            if (camAngle >= 45 && camAngle < 135)
            {
                correctionAngle = 90;
            }
            else if (camAngle >= 135 && camAngle < 225)
            {
                correctionAngle = 180;
            }
            else if (camAngle >= 225 && camAngle < 312)
            {
                correctionAngle = 270;
            }

            var radCorrectionAngle = Mathf.Deg2Rad * correctionAngle;
            return radCorrectionAngle;
        }

        private void Move(Vector3 dir)
        {
            theRoomBehaviour.selectedElement.transform.position = theRoomBehaviour.selectedElement.transform.position + dir;
            currentInterval = moveInterval;
            theRoomBehaviour.selectedElement.UpdateIsPlaceable();
        }

        private void Rotate(Vector3 dir)
        {
            theRoomBehaviour.selectedElement.transform.Rotate(dir);
            currentInterval = moveInterval;
            theRoomBehaviour.selectedElement.UpdateIsPlaceable();
        }


        public void CamRotateYN()
        {
            if (!isCamRotating)
            {
                var rotationChange = Quaternion.Euler(0, 0, -15);
                SetCamRotation(rotationChange, true);
            }
            //CamRotate(new Vector3(0, 0, -15));
        }
        public void CamRotateYP()
        {
            if (!isCamRotating)
            {
                var rotationChange = Quaternion.Euler(0, 0, 15);
                SetCamRotation(rotationChange, true);
            }
            //CamRotate(new Vector3(0, 0, 15));
        }

        private Quaternion rotationTarget;
        private Quaternion rotationStart;
        private float rotationLerpPercentage = 1f;
        private bool isCamRotating = false;

        public void CamRotateZN()
        {
            if (!isCamRotating)
            {
                var rotationChange = Quaternion.Euler(0, -15, 0);
                SetCamRotation(rotationChange, false);
            }
            //CamRotate(new Vector3(0, -15, 0), Space.World);
        }

        public void CamRotateZP()
        {
            if (!isCamRotating)
            {
                var rotationChange = Quaternion.Euler(0, 15, 0);
                SetCamRotation(rotationChange, false);
            }
        }

        public void CamRotateXN()
        {
            //var forward = cam.transform.forward;
            //CamRotate(new Vector3(-15, 0, 0));
            //var currEulerAngles = cam.transform.eulerAngles;
            //var x = currEulerAngles.x - 15;
            //if (x > 90 && x < 270)
            //{
            //    x = -90;
            //}
            //currEulerAngles.x = x;
            //cam.transform.rotation = Quaternion.Euler(currEulerAngles);

            if (!isCamRotating)
            {
                var rotationChange = Quaternion.Euler(-15, 0, 0);
                SetCamRotation(rotationChange, false);
            }
        }

        public void CamRotateXP()
        {
            //CamRotate(new Vector3(15, 0, 0));
            //var currEulerAngles = cam.transform.eulerAngles;
            //var x = currEulerAngles.x + 15;

            //if (x > 90 && x < 270)
            //{
            //    x = 90;
            //}
            //currEulerAngles.x = x;
            //cam.transform.rotation = Quaternion.Euler(currEulerAngles);
            if (!isCamRotating)
            {
                var rotationChange = Quaternion.Euler(15, 0, 0);
                SetCamRotation(rotationChange, false);
            }
        }

        public void CamMoveDown()
        {
            CamMove(new Vector3(0, -1, 0), Space.World);
        }

        public void CamMoveUp()
        {
            CamMove(new Vector3(0, 1, 0), Space.World);
        }

        public void CamMoveRight()
        {
            //var radCorrectionAngle = GetCorrectionAngle();
            float camAngle = Mathf.Deg2Rad * cam.transform.eulerAngles.y;
            //CamMove(new Vector3(Mathf.Sin(-camAngle), 0, Mathf.Cos(-camAngle)));
            var v = new Vector3(Mathf.Cos(camAngle), 0, -Mathf.Sin(camAngle));
            CamMove(v);

            //CamMove(new Vector3(1, 0, 0));
        }

        public void CamMoveLeft()
        {
            //var radCorrectionAngle = GetCorrectionAngle();
            float camAngle = Mathf.Deg2Rad * cam.transform.eulerAngles.y;
            //CamMove(new Vector3(-Mathf.Sin(-camAngle), 0, -Mathf.Cos(-camAngle)));
            CamMove(new Vector3(-Mathf.Cos(camAngle), 0, Mathf.Sin(camAngle)));

            //CamMove(new Vector3(-1, 0, 0));
        }

        public void CamMoveBack()
        {
            float camAngle = Mathf.Deg2Rad * cam.transform.eulerAngles.y;
            CamMove(new Vector3(-Mathf.Sin(camAngle), 0, -Mathf.Cos(camAngle)), Space.World);

            //CamMove(new Vector3(0, -1, 0));
        }

        public void CamMoveForeward()
        {
            float camAngle = Mathf.Deg2Rad * cam.transform.eulerAngles.y;
            CamMove(new Vector3(Mathf.Sin(camAngle), 0, Mathf.Cos(camAngle)), Space.World);

            //CamMove(new Vector3(0, 1, 0));
        }

        private void CamMove(Vector3 dir, Space relativeTo = Space.Self)
        {
            cam.transform.position = cam.transform.position + dir;
            //cam.transform.Translate(dir, relativeTo);
        }

        private void CamRotate(Vector3 dir, Space relativeTo = Space.Self)
        {
            cam.transform.Rotate(dir, relativeTo);
        }


        private void SetCamRotation(Quaternion rotationChange, bool selfCentred)
        {
            rotationStart = cam.transform.rotation;
            rotationTarget = rotationStart;
            if (selfCentred)
            {
                rotationTarget = rotationTarget * rotationChange;
            }
            else
            {
                rotationTarget = rotationChange * rotationTarget;
            }
            rotationLerpPercentage = 0;
            Debug.Log("Start: " + rotationStart.eulerAngles);
            Debug.Log("End: " + rotationTarget.eulerAngles);
            isCamRotating = true;
        }

        public void Update()
        {
            if (isCamRotating)
            {
                rotationLerpPercentage = Mathf.MoveTowards(rotationLerpPercentage, 1f, Time.deltaTime * 10f);
                if (rotationLerpPercentage >= 1)
                {
                    cam.transform.rotation = rotationTarget;
                    isCamRotating = false;
                }
                else
                {
                    cam.transform.rotation = Quaternion.Slerp(rotationStart, rotationTarget, rotationLerpPercentage);
                }

            }
        }


    }
}
