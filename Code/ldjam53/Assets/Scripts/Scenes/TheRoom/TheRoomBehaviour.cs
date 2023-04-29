using System;
using System.Collections.Generic;

using Assets.Scripts.Constants;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;

using GameFrame.Core.Extensions;

using UnityEngine;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class TheRoomBehaviour : MonoBehaviour
    {
        private readonly Dictionary<String, GameObject> availableModels = new Dictionary<String, GameObject>();
        private GameObject objectsContainer;

        public GameObject template;
        public RoomElementBehaviour selectedElement;

        public void ToMainMenu()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.MainMenu);
        }

        public void Awake()
        {
            this.objectsContainer = transform.Find("ObjectsContainer").gameObject;
            LoadTemplates();
        }

        public void TestLoad()
        {
            var room = Base.Core.Game.AvailableGameModes[0].TheRoom;
            LoadRoom(room);
        }

        public void LoadBox()
        {
            var box = new RoomElement("BoxDefault")
            {
                Model = "Pack_box_edge",
                Rotatable = true,
                Rotation = GameFrame.Core.Math.Vector3.Zero,
            };

            AddRoomElement(box, new UnityEngine.Vector3(1, 1, 1));
        }

        public void LoadBoxLong()
        {
            var longBox = new RoomElement("LongBox")
            {
                Model = "Pack_box_long_edge",
                Rotatable = true,
                Rotation = GameFrame.Core.Math.Vector3.Zero
            };

            AddRoomElement(longBox, new UnityEngine.Vector3(1, 1, 1));
        }

        public void LoadBroom()
        {
            var longBox = new RoomElement("Broom")
            {
                Model = "Broom_edge",
                Rotatable = true,
                Rotation = GameFrame.Core.Math.Vector3.Zero
            };

            AddRoomElement(longBox, new UnityEngine.Vector3(1, 1, 1));
        }

        public void LoadLetter()
        {
            var longBox = new RoomElement("Letter")
            {
                Model = "Pack_letter_edge",
                Rotatable = true,
                Rotation = GameFrame.Core.Math.Vector3.Zero
            };

            AddRoomElement(longBox, new UnityEngine.Vector3(1, 1, 1));
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
                            var mat = Instantiate(template, objectsContainer.transform);

                            mat.AddRoomElement(roomElement);

                            mat.transform.position = new UnityEngine.Vector3(x, y, z);
                            mat.SetActive(true);
                        }
                    }
                }
            }
        }

        private void AddRoomElement(RoomElement roomElement, UnityEngine.Vector3 position, Boolean setSelected = true)
        {
            if (roomElement.Model.HasValue() && availableModels.TryGetValue(roomElement.Model, out var modelTemplate))
            {
                var mat = Instantiate(modelTemplate, objectsContainer.transform);

                var roomElementBehaviour = mat.AddRoomElement(roomElement);

                mat.transform.position = position;
                mat.SetActive(true);

                if (setSelected)
                {
                    SetSelectedElement(roomElementBehaviour);
                }
            }
            else
            {

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

        private void LoadTemplates()
        {
            var templateConatiner = transform.Find("Templates");

            if (templateConatiner != default)
            {
                LoadTemplates(this.availableModels, templateConatiner, "Objects");
            }
        }

        private void LoadTemplates<T>(IDictionary<String, T> cache, Transform rootTemplateContainer, String templateContainerName)
        {
            var templateGameObjects = rootTemplateContainer.transform.Find(templateContainerName).gameObject;

            if (templateGameObjects.transform.childCount > 0)
            {
                foreach (Transform buildingTemplate in templateGameObjects.transform)
                {
                    if (buildingTemplate.gameObject is T castedObject)
                    {
                        cache[buildingTemplate.name] = castedObject;
                    }
                }
            }
            else
            {
                throw new Exception($"Missing '{templateContainerName}' templates!");
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