using System.Collections;
using System.Collections.Generic;

using UnityEngine;



namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class ManipulatorBehaviour : MonoBehaviour
    {
        public Camera cam;

        public TheRoomBehaviour theRoomBehaviour;
        private float moveInterval = 0.1f;
        private float currentInterval = 0f;

        public void Update()
        {
            if (currentInterval <= 0)
            {
                if (theRoomBehaviour.selectedElement != default)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Move(new Vector3(Mathf.Sin(radCorrectionAngle), 0, Mathf.Cos(radCorrectionAngle)));
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Move(new Vector3(-Mathf.Sin(radCorrectionAngle), 0, -Mathf.Cos(radCorrectionAngle)));
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Move(new Vector3(-Mathf.Cos(-radCorrectionAngle), 0, -Mathf.Sin(-radCorrectionAngle)));
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Move(new Vector3(Mathf.Cos(-radCorrectionAngle), 0, Mathf.Sin(-radCorrectionAngle)));
                    }
                    else if (Input.GetKey(KeyCode.Q))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Move(new Vector3(0, 1, 0));
                    }
                    else if (Input.GetKey(KeyCode.E))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Move(new Vector3(0, -1, 0));
                    }
                    else if (Input.GetKey(KeyCode.Z))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Rotate(new Vector3(90, 0, 0));
                    }
                    else if (Input.GetKey(KeyCode.X))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Rotate(new Vector3(0, 90, 0));
                    }
                    else if (Input.GetKey(KeyCode.C))
                    {
                        var radCorrectionAngle = GetCorrectionAngle();
                        Rotate(new Vector3(0, 0, 90));
                    }
                }
            }
            else
            {
                currentInterval -= Time.deltaTime;
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

        private void Move(Vector3 dir)
        {
            theRoomBehaviour.selectedElement.transform.position = theRoomBehaviour.selectedElement.transform.position + dir;
            currentInterval = moveInterval;
            theRoomBehaviour.selectedElement.UpdateIsPlaceable();
            //Debug.Log("New Position: " + theRoomBehaviour.selectedElement.transform.position);
        }

        private void Rotate(Vector3 dir)
        {

            //var pos = theRoomBehaviour.selectedElement.transform.position;
            //var rotationPoint = pos + theRoomBehaviour.selectedElement.transform.rotation * new Vector3(0.5f, 0.5f, 0.5f);
            //Debug.Log("Position: " + theRoomBehaviour.selectedElement.transform.position);
            //Debug.Log("Rotate around: " + rotationPoint);
            //theRoomBehaviour.SpawnFromKey("Wall", rotationPoint);
            //theRoomBehaviour.selectedElement.transform.RotateAround(rotationPoint, dir, 90);
            theRoomBehaviour.selectedElement.transform.Rotate(dir);
            currentInterval = moveInterval;
            //theRoomBehaviour.selectedElement.transform.position = pos;
            theRoomBehaviour.selectedElement.UpdateIsPlaceable();
            //Debug.Log("New Position: " + theRoomBehaviour.selectedElement.transform.position);
        }


    }
}
