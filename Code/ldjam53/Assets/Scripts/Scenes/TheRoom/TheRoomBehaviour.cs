using Assets.Scripts.Constants;
using UnityEngine;
using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Scenes
{
    public class TheRoomBehaviour : MonoBehaviour
    {
        public GameObject template;

        public List<GameObject> models;

        private float scale;
        private Dictionary<string, GameObject> modelDict;
        private GameObject selectedObject;


        public void ToMainMenu()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.MainMenu);
        }


        public void Awake()
        {
            RectTransform rt = (RectTransform)template.transform;
            scale = rt.rect.width;
            modelDict = new Dictionary<string, GameObject>();
            foreach (var model in models)
            {
                modelDict.Add(model.name, model);
            }
        }



        public void TestLoad()
        {
            Room room = new Room();
            room.Materials = new RoomElement[20, 20, 20];
            var ix = room.Materials.GetLength(0) - 1;
            for (int x = 0; x < room.Materials.GetLength(0); x++)
            {
                for (int z = 0; z < room.Materials.GetLength(2); z++)
                {
                    room.Materials[x, 0, z] = new RoomElement("floor");
                }
                for (int y = 0; y < room.Materials.GetLength(1); y++)
                {
                    room.Materials[x, y, ix] = new RoomElement("wall xy");
                }
            }

            for (int z = 0; z < room.Materials.GetLength(2); z++)
            {
                for (int y = 0; y < room.Materials.GetLength(1); y++)
                {
                    room.Materials[ix, y, z] = new RoomElement("wall zy");
                }
            }
            LoadRoom(room);
        }


        public void LoadBox()
        {
            var mat = Instantiate(modelDict["Pack_box"], template.transform.parent);
            mat.name = "Box";
            mat.transform.position = new UnityEngine.Vector3(1, 1, 1);
            mat.SetActive(true);
        }

        public void LoadBoxLong()
        {
            var mat = Instantiate(modelDict["Pack_box_long"], template.transform.parent);
            mat.name = "Box";
            mat.transform.position = new UnityEngine.Vector3(1, 1, 1);
            mat.SetActive(true);
        }

        public void LoadLetter()
        {
            var mat = Instantiate(modelDict["Pack_letter"], template.transform.parent);
            mat.name = "Box";
            mat.transform.position = new UnityEngine.Vector3(1, 1, 1);
            mat.SetActive(true);
        }

        public void LoadRoom(Room room)
        {
            for (int x = 0; x < room.Materials.GetLength(0); x++)
            {
                for (int y = 0; y < room.Materials.GetLength(1); y++)
                {
                    for (int z = 0; z < room.Materials.GetLength(2); z++)
                    {
                        var roomMaterial = room.Materials[x, y, z];
                        if (roomMaterial != default)
                        {
                            var mat = Instantiate(template, template.transform.parent);
                            mat.name = string.Format("{0} x:{1} y:{2} z:{3}", roomMaterial.texture, x, y, z);
                            mat.transform.position = new UnityEngine.Vector3(x, y, z);
                            mat.SetActive(true);
                        }
                    }
                }
            }
        }


        //private void LateUpdate()
        //{
        //    if (Input.GetMouseButtonUp(0)) //!Base.Core.Game.LockCameraMovement && 
        //    {
        //        if (!EventSystem.current.IsPointerOverGameObject())    // is the touch on the GUI
        //        {
        //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //            if (Physics.Raycast(ray, out var raycastHit, 100.0f))
        //            {
        //                if (raycastHit.transform.gameObject != null)
        //                {
        //                    selectedObject = raycastHit.transform.gameObject;        
                            
        //                }
        //            }
        //        }
        //    }

        //}
    }
}