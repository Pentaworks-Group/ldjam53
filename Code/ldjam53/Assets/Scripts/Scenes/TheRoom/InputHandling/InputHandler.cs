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

        private Quaternion camRotationTarget;
        private Quaternion camRotationStart;
        private float camRotationLerpPercentage = 1f;
        private bool isCamRotating = false;

        public void Update()
        {
            if (isCamRotating)
            {
                camRotationLerpPercentage = Mathf.MoveTowards(camRotationLerpPercentage, 1f, Time.deltaTime * 2f);
                if (camRotationLerpPercentage >= 1)
                {
                    cam.transform.rotation = camRotationTarget;
                    isCamRotating = false;
                }
                else
                {
                    cam.transform.rotation = Quaternion.Slerp(camRotationStart, camRotationTarget, camRotationLerpPercentage);
                }

            }
        }

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
        }
        public void CamRotateYP()
        {
            if (!isCamRotating)
            {
                var rotationChange = Quaternion.Euler(0, 0, 15);
                SetCamRotation(rotationChange, true);
            }
        }



        public void CamRotateZN()
        {
            if (!isCamRotating)
            {
                var rotationChange = Quaternion.Euler(0, -15, 0);
                SetCamRotation(rotationChange, false);
            }
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

            if (!isCamRotating)
            {

                camRotationStart = cam.transform.rotation;
                var rotationChange = Quaternion.Euler(-15, 0, 0);
                var axis = Vector3.Cross(cam.transform.forward, Vector3.up);

                camRotationTarget =  Quaternion.AngleAxis(15, axis) * camRotationStart;
                camRotationLerpPercentage = 0;
                isCamRotating = true;

                Debug.Log("Start: " + camRotationStart.eulerAngles);
                Debug.Log("End: " + camRotationTarget.eulerAngles);
                //SetCamRotation(rotationChange, false);
            }
        }

        public void CamRotateXP()
        {
            if (!isCamRotating)
            {
                camRotationStart = cam.transform.rotation;

                var axis = Vector3.Cross(cam.transform.forward, Vector3.up);

                camRotationTarget = Quaternion.AngleAxis(-15, axis) * camRotationStart;
                camRotationLerpPercentage = 0;
                isCamRotating = true;
                Debug.Log("Start: " + camRotationStart.eulerAngles);
                Debug.Log("End: " + camRotationTarget.eulerAngles);
                //var rotationChange = Quaternion.Euler(15, 0, 0);
                //SetCamRotation(rotationChange, false);
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

        private void SetCamRotation(Quaternion rotationChange, bool selfCentred)
        {
            camRotationStart = cam.transform.rotation;
            camRotationTarget = camRotationStart;
            if (selfCentred)
            {
                camRotationTarget = camRotationTarget * rotationChange;
            }
            else
            {
                camRotationTarget = rotationChange * camRotationTarget;
            }
            camRotationLerpPercentage = 0;
            //Debug.Log("Start: " + camRotationStart.eulerAngles);
            //Debug.Log("End: " + camRotationTarget.eulerAngles);
            isCamRotating = true;
        }
    }
}
