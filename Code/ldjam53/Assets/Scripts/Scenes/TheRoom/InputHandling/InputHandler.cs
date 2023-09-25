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


        private MoveInputHandler selectedMoveHandler = new();
        private MoveInputHandler camMoveHandler = new();

        private RotateInputHandler selectedRotateHandler = new();
        private RotateInputHandler camRotateHandler = new();

        private TouchMouseHandler touchMouseHandler = new();


        public void Update()
        {
            camMoveHandler.UpdateHandler();
            selectedMoveHandler.UpdateHandler();


            camRotateHandler.UpdateHandler();
            selectedRotateHandler.UpdateHandler();

            touchMouseHandler.UpdateTouchHandler();
        }

        public void Start()
        {
            camMoveHandler.Init(cam.transform, null);
            camRotateHandler.Init(cam.transform, null);
            selectedRotateHandler.SetAngle(90);
            touchMouseHandler.Init(Lefti, Righti, Zoomi);
        }


        private void Zoomi(Vector2 vector)
        {
            Debug.Log(Base.Core.Game.Options.ZoomSensivity);
            Vector3 newVect = cam.transform.forward * Time.deltaTime * Base.Core.Game.Options.ZoomSensivity * 20.0f * vector.y;
            Vector3 newCamPos = cam.transform.position + newVect;
            if (newCamPos.y < 1)
            {
                return;
            }

            cam.transform.position = newCamPos;
        }
        private void Righti(Vector2 vector)
        {
            cam.transform.Rotate(new Vector3(-vector.y, vector.x, 0) * Time.deltaTime * Base.Core.Game.Options.LookSensivity);
            float z = cam.transform.eulerAngles.z;
            cam.transform.Rotate(0, 0, -z);
        }

        private void Lefti(Vector2 vector)
        {
            cam.transform.position += ApplyCamCorrection(-vector.x, 0, -vector.y) * Time.deltaTime * Base.Core.Game.Options.MoveSensivity;
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

        private float GetCamCorrectionAngle()
        {
            float camAngle = cam.transform.eulerAngles.y;
            return Mathf.Deg2Rad * camAngle;
        }


        public void RotateYN()
        {
            if (selectedRotateHandler.IsRotating() == false)
            {
                RotateSelected(new Vector3(0, 0, -90));
            }
        }
        public void RotateYP()
        {
            if (selectedRotateHandler.IsRotating() == false)
            {
                RotateSelected(new Vector3(0, 0, 90));
            }
        }

        public void RotateZN()
        {
            if (selectedRotateHandler.IsRotating() == false)
            {
                RotateSelected(new Vector3(0, -90, 0));
            }
        }

        public void RotateZP()
        {
            if (selectedRotateHandler.IsRotating() == false)
            {
                RotateSelected(new Vector3(0, 90, 0));
            }
        }

        public void RotateXN()
        {
            if (selectedRotateHandler.IsRotating() == false)
            {
                RotateSelected(new Vector3(-90, 0, 0));
            }
        }

        public void RotateXP()
        {
            if (selectedRotateHandler.IsRotating() == false)
            {
                RotateSelected(new Vector3(90, 0, 0));
            }
        }

        public void MoveDown()
        {
            if (selectedMoveHandler.IsMoving() == false)
            {
                MoveSelected(new Vector3(0, -1, 0));
            }
        }

        public void MoveUp()
        {
            if (selectedMoveHandler.IsMoving() == false)
            {
                MoveSelected(new Vector3(0, 1, 0));
            }
        }

        public void MoveRight()
        {
            if (selectedMoveHandler.IsMoving() == false)
            {
                var radCorrectionAngle = GetCorrectionAngle();
                MoveSelected(new Vector3(Mathf.Cos(-radCorrectionAngle), 0, Mathf.Sin(-radCorrectionAngle)));
            }
        }

        public void MoveLeft()
        {
            if (selectedMoveHandler.IsMoving() == false)
            {
                var radCorrectionAngle = GetCorrectionAngle();
                MoveSelected(new Vector3(-Mathf.Cos(-radCorrectionAngle), 0, -Mathf.Sin(-radCorrectionAngle)));
            }
        }

        public void MoveBack()
        {
            if (selectedMoveHandler.IsMoving() == false)
            {
                var radCorrectionAngle = GetCorrectionAngle();
                MoveSelected(new Vector3(-Mathf.Sin(radCorrectionAngle), 0, -Mathf.Cos(radCorrectionAngle)));
            }
        }

        public void MoveForeward()
        {
            if (selectedMoveHandler.IsMoving() == false)
            {
                var radCorrectionAngle = GetCorrectionAngle();
                MoveSelected(new Vector3(Mathf.Sin(radCorrectionAngle), 0, Mathf.Cos(radCorrectionAngle)));
            }
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

        private void MoveSelected(Vector3 dir)
        {
            selectedMoveHandler.StartMove(dir, theRoomBehaviour.selectedElement.transform, theRoomBehaviour.selectedElement.UpdateIsPlaceable);
        }

        private void RotateSelected(Vector3 dir)
        {
            selectedRotateHandler.StartRotation(dir, theRoomBehaviour.selectedElement.transform, theRoomBehaviour.selectedElement.UpdateIsPlaceable);
        }


        public void CamRotateYN()
        {
            if (camRotateHandler.IsRotating() == false)
            {
                var axis = cam.transform.forward * -1;
                RotateCam(axis);
            }
        }
        public void CamRotateYP()
        {
            if (camRotateHandler.IsRotating() == false)
            {
                var axis = cam.transform.forward;
                RotateCam(axis);
            }
        }
        public void CamRotateZN()
        {
            if (camRotateHandler.IsRotating() == false)
            {
                var axis = Vector3.down;
                RotateCam(axis);
            }
        }

        public void CamRotateZP()
        {
            if (camRotateHandler.IsRotating() == false)
            {
                var axis = Vector3.up;
                RotateCam(axis);
            }
        }

        public void CamRotateXN()
        {

            if (camRotateHandler.IsRotating() == false)
            {
                var axis = Vector3.Cross(cam.transform.forward, Vector3.up);
                RotateCam(axis);
            }
        }

        public void CamRotateXP()
        {
            if (camRotateHandler.IsRotating() == false)
            {
                var axis = Vector3.Cross(cam.transform.forward, Vector3.up);

                RotateCam(axis * -1);
            }
        }

        private void RotateCam(Vector3 axis)
        {
            camRotateHandler.StartRotation(axis, cam.transform.rotation);
        }

        public void CamMoveDown()
        {
            if (camMoveHandler.IsMoving() == false)
            {
                CamMove(new Vector3(0, -1, 0));
            }
        }

        public void CamMoveUp()
        {
            if (camMoveHandler.IsMoving() == false)
            {
                CamMove(new Vector3(0, 1, 0));
            }
        }

        public void CamMoveRight()
        {
            if (camMoveHandler.IsMoving() == false)
            {
                float camAngle = Mathf.Deg2Rad * cam.transform.eulerAngles.y;
                var v = new Vector3(Mathf.Cos(camAngle), 0, -Mathf.Sin(camAngle));
                CamMove(v);
            }
        }

        public void CamMoveLeft()
        {
            if (camMoveHandler.IsMoving() == false)
            {
                float camAngle = Mathf.Deg2Rad * cam.transform.eulerAngles.y;
                CamMove(new Vector3(-Mathf.Cos(camAngle), 0, Mathf.Sin(camAngle)));
            }
        }

        public void CamMoveBack()
        {
            if (camMoveHandler.IsMoving() == false)
            {
                float camAngle = Mathf.Deg2Rad * cam.transform.eulerAngles.y;
                CamMove(new Vector3(-Mathf.Sin(camAngle), 0, -Mathf.Cos(camAngle)));
            }
        }

        public void CamMoveForeward()
        {
            if (camMoveHandler.IsMoving() == false)
            {
                float camAngle = Mathf.Deg2Rad * cam.transform.eulerAngles.y;
                CamMove(new Vector3(Mathf.Sin(camAngle), 0, Mathf.Cos(camAngle)));
            }
        }

        private void CamMove(Vector3 dir)
        {
            camMoveHandler.StartMove(dir, cam.transform.position);
        }
    }
}
