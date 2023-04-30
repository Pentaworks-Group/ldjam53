using System;
using System.Collections.Generic;
using System.IO;

using Assets.Scripts.Constants;
using Assets.Scripts.Core.Definitions;
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

        public Camera sceneCamera;
        public RoomElementBehaviour selectedElement;

        private UnityEngine.Vector3 spawn = new UnityEngine.Vector3(2, 2, 2);


        public RoomType CurrentRoomType { get; private set; }



        public void Awake()
        {
            this.objectsContainer = transform.Find("ObjectsContainer").gameObject;
            LoadTemplates();
        }


        public void Start()
        {
            LoadCurrentRoom();
        }


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlaceSelected();
            }
        }


        public void ToMainMenu()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.MainMenu);
        }


        public void PlaceSelected()
        {
            if (selectedElement == default || selectedElement.IsPlaceable)
            {
                var level = GetCurrentLevelDefinition();
                if (level.IsSelectionRandom)
                {
                    SpawnRandomFromRemainingElement();
                } else
                {

                }

            }
        }

        public void SpawnFromKey(String key)
        {
            var roomElementType = Base.Core.Game.State.GameMode.ElementTypes[key];
            var roomElement = new RoomElement(roomElementType.Name)
            {
                Model = roomElementType.Models.GetRandomEntry(),
                IconReference = roomElementType.IconReference,
                Material = roomElementType.Materials.GetRandomEntry()
                //Rotatable = roomElementType.Rotatable
            };
            AddRoomElement(roomElement, spawn);
        }

        private void SpawnRandomFromRemainingElement()
        {
            var remainingElements = Base.Core.Game.State.CurrentLevel.RemainingElements;
            List<String> elements = new List<String>(remainingElements.Keys);
            var rndKey = elements.GetRandomEntry();
            remainingElements[rndKey]--;
            if (remainingElements[rndKey] < 1)
            {
                remainingElements.Remove(rndKey);
            }
            SpawnFromKey(rndKey);
        }



        public void BuildRoom()
        {
            int sizeX = 12;
            int sizeY = 12;
            int sizeZ = 26;
            RoomType roomType = new RoomType
            {
                WallElements = new List<WallElement>(),
                Name = "Cask",
                Size = new GameFrame.Core.Math.Vector3(sizeX, sizeY, sizeZ)

            };
            var wall = new WallElement()
            {
                Positions = new List<GameFrame.Core.Math.Vector3>(),
                Model = "Singleblock"
            };
            roomType.WallElements.Add(wall);
            //var roof = new WallElement();
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    wall.Positions.Add(new GameFrame.Core.Math.Vector3(x, y, 0));
                    wall.Positions.Add(new GameFrame.Core.Math.Vector3(x, y, sizeZ - 1));
                }
                for (int z = 0; z < sizeZ; z++)
                {
                    wall.Positions.Add(new GameFrame.Core.Math.Vector3(x, 0, z));
                    //wall.Positions.Add(new GameFrame.Core.Math.Vector3(x, sizeY - 1, z));
                }
            }
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    wall.Positions.Add(new GameFrame.Core.Math.Vector3(0, y, z));
                    wall.Positions.Add(new GameFrame.Core.Math.Vector3(sizeX - 1, y, z));
                }
            }
            DumpRoom(roomType);
        }

        private void DumpRoom(RoomType room)
        {
            var json = GameFrame.Core.Json.Handler.SerializePretty(room);
            var filePath = Application.streamingAssetsPath + "/room.json";
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.Write(json);
            writer.Close();
        }

        private void LoadCurrentRoom()
        {
            CurrentRoomType = GetRoomTypeByLevelId();
            foreach (var wallElem in CurrentRoomType.WallElements)
            {
                availableModels.TryGetValue(wallElem.Model, out var modelTemplate);
                foreach (var position in wallElem.Positions)
                {
                    InstantiateWallElement(position, modelTemplate);
                }
            }

            AdjustCamera();
            if (Base.Core.Game.State.CurrentLevel.TheRoom.Elements == default)
            {
                Base.Core.Game.State.CurrentLevel.TheRoom.Elements = new List<RoomElement>();
            }
            foreach (var roomElement in Base.Core.Game.State.CurrentLevel.TheRoom.Elements)
            {
                AddRoomElement(roomElement, setSelected: false);
            }
        }

        private void InstantiateWallElement(GameFrame.Core.Math.Vector3 position, GameObject template)
        {
            var mat = Instantiate(template, objectsContainer.transform);

            //mat.AddRoomElement(roomElement);
            mat.AddComponent<BoundsCalculationBehaviour>();
            mat.transform.position = position.ToUnity();
            mat.SetActive(true);
        }

        private RoomType GetRoomTypeByLevelId()
        {
            var level = GetCurrentLevelDefinition();
            if (level != null)
            {
                return Base.Core.Game.State.GameMode.RoomTypes[level.RoomReference];
            }
            return default;
        }

        private static LevelDefinition GetCurrentLevelDefinition()
        {
            Guid iD = Base.Core.Game.State.CurrentLevel.ID;
            Base.Core.Game.State.GameMode.LevelsByID.TryGetValue(iD, out var level);
            return level;
        }




        //private void MergeWallMeshes(List<GameObject> wallObjects)
        //{

        //    //MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        //    CombineInstance[] combine = new CombineInstance[wallObjects.Count];

        //    int i = 0;
        //    while (i < wallObjects.Count)
        //    {
        //        combine[i].mesh = wallObjects[i].GetComponent<MeshFilter>().sharedMesh;
        //        combine[i].transform = wallObjects[i].transform.localToWorldMatrix;
        //        wallObjects[i].gameObject.SetActive(false);

        //        i++;
        //    }

        //    Mesh mesh = new Mesh();
        //    mesh.CombineMeshes(combine);
        //    cube.transform.GetComponent<MeshFilter>().sharedMesh = mesh;
        //    //transform.gameObject.SetActive(true);
        //}

        private void AddRoomElement(RoomElement roomElement, Boolean setSelected = true)
        {
            if (roomElement.Model.HasValue() && availableModels.TryGetValue(roomElement.Model, out var modelTemplate))
            {
                var mat = Instantiate(modelTemplate, objectsContainer.transform);

                var roomElementBehaviour = mat.AddRoomElement(roomElement, this);

                mat.transform.position = roomElement.Position.ToUnity();
                mat.SetActive(true);

                if (setSelected)
                {
                    SetSelectedElement(roomElementBehaviour);
                }
            }
        }

        private void AddRoomElement(RoomElement roomElement, UnityEngine.Vector3 position, Boolean setSelected = true)
        {
            if (roomElement.Model.HasValue() && availableModels.TryGetValue(roomElement.Model, out var modelTemplate))
            {
                var mat = Instantiate(modelTemplate, objectsContainer.transform);

                var roomElementBehaviour = mat.AddRoomElement(roomElement, this);

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


        public void AdjustCamera()
        {
            Bounds b = GetBounds(this.gameObject);

            float cameraDistance = .25f; // Constant factor
            UnityEngine.Vector3 objectSizes = b.max - b.min;

            float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);

            float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * sceneCamera.fieldOfView); // Visible height 1 meter in front
            float distance = cameraDistance * objectSize / cameraView; // Combined wanted distance from the object
            distance += 0.5f * objectSize; // Estimated offset from the center to the outside of the object
            sceneCamera.transform.position = b.center - distance * sceneCamera.transform.forward;
        }

        internal static Bounds GetBounds(GameObject gameObject)
        {
            Bounds b = new Bounds(gameObject.transform.position, UnityEngine.Vector3.zero);

            b = GetBoundRec(gameObject.transform, b);

            return b;
        }

        internal static Bounds GetBoundRec(Transform goT, Bounds b)
        {
            var boundsBehaviour = goT.GetComponent<BoundsCalculationBehaviour>();

            if (boundsBehaviour == default || boundsBehaviour.IsIncluded)
            {
                foreach (Transform child in goT)
                {
                    if (child.gameObject.activeSelf)
                    {
                        b = GetBoundRec(child, b);

                        Renderer r = child.GetComponent<MeshRenderer>();

                        if (r != default)
                        {
                            b.Encapsulate(r.bounds);
                            //Debug.Log("dodi", child);
                        }
                        else
                        {
                            //Debug.Log("No render:", child);
                        }
                    }
                }
            }

            return b;
        }



    }
}