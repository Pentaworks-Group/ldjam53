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


    }
}
