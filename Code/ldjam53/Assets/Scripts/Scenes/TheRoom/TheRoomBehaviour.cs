using System;
using System.Collections.Generic;

using Assets.Scripts.Constants;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using Assets.Scripts.Scenes.TheRoom;

using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class TheRoomBehaviour : MonoBehaviour
    {
        private Int32 counter = 0;

        public GameObject template;

        public List<GameObject> models;

        private float scale;
        private Dictionary<string, GameObject> modelDict;
        private RoomElementBehaviour selectedElement;

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
            Room room = new Room
            {
                Materials = new RoomElement[20, 20, 20]
            };

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
            var box = new RoomElement("BoxDefault")
            {
                Model = "Test",
                Rotatable = true,
                Rotation = GameFrame.Core.Math.Vector3.Zero,
            };

            counter++;

            AddRoomElement(box, new Vector3(counter, 1, 1));
        }

        public void LoadBoxLong()
        {
            var longBox = new RoomElement("LongBox")
            {
                Model = "Pack_box_long",
                Rotatable = true,
                Rotation = GameFrame.Core.Math.Vector3.Zero
            };

            counter++;

            AddRoomElement(longBox, new Vector3(counter, 1, 1));
        }

        public void LoadLetter()
        {
            var longBox = new RoomElement("Letter")
            {
                Model = "Pack_letter",
                Rotatable = true,
                Rotation = GameFrame.Core.Math.Vector3.Zero
            };

            counter++;

            AddRoomElement(longBox, new Vector3(counter, 1, 1));
        }

        public void LoadRoom(Room room)
        {
            for (int x = 0; x < room.Materials.GetLength(0); x++)
            {
                for (int y = 0; y < room.Materials.GetLength(1); y++)
                {
                    for (int z = 0; z < room.Materials.GetLength(2); z++)
                    {
                        var roomElement = room.Materials[x, y, z];

                        if (roomElement != default)
                        {
                            var mat = Instantiate(template, template.transform.parent);

                            mat.AddRoomElement(roomElement);

                            mat.transform.position = new UnityEngine.Vector3(x, y, z);
                            mat.SetActive(true);
                        }
                    }
                }
            }
        }

        private void AddRoomElement(RoomElement roomElement, Vector3 position, Boolean setSelected = true)
        {
            var mat = Instantiate(modelDict[roomElement.Model], template.transform.parent);

            var roomElementBehaviour = mat.AddRoomElement(roomElement);

            mat.transform.position = position;
            mat.SetActive(true);

            if (setSelected)
            {
                SetSelectedElement(roomElementBehaviour);
            }
        }

        private void SetSelectedElement(RoomElementBehaviour roomElementBehaviour)
        {
            if (this.selectedElement != null)
            {
                this.selectedElement.SetSelected(false);
            }

            this.selectedElement = roomElementBehaviour;

            this.selectedElement.SetSelected(true);
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