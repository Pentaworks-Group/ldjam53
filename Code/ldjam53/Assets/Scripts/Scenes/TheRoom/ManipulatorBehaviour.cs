using System.Collections;
using System.Collections.Generic;

using UnityEngine;



namespace Assets.Scripts.Scenes.TheRoom
{
    public class ManipulatorBehaviour : MonoBehaviour
    {
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
                        Move(new Vector3(1, 0, 0));
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        Move(new Vector3(-1, 0, 0));
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        Move(new Vector3(0, 0, -1));
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        Move(new Vector3(0, 0, 1));
                    }
                    else if (Input.GetKey(KeyCode.Q))
                    {
                        Move(new Vector3(0, 1, 0));
                    }
                    else if (Input.GetKey(KeyCode.E))
                    {
                        Move(new Vector3(0, -1, 0));
                    }
                    else if (Input.GetKey(KeyCode.Z))
                    {
                        Rotate(new Vector3(90, 0, 0));
                    }
                    else if (Input.GetKey(KeyCode.X))
                    {
                        Rotate(new Vector3(0, 90, 0));
                    }
                    else if (Input.GetKey(KeyCode.C))
                    {
                        Rotate(new Vector3(0, 0, 90));
                    }
                }
            } else
            {
                currentInterval -= Time.deltaTime;
            }
        }

        private void Move(Vector3 dir)
        {
            theRoomBehaviour.selectedElement.transform.position = theRoomBehaviour.selectedElement.transform.position + dir;
            currentInterval = moveInterval;
        }

        private void Rotate(Vector3 dir)
        {
            theRoomBehaviour.selectedElement.transform.Rotate(dir);
            currentInterval = moveInterval;
        }


    }
}
