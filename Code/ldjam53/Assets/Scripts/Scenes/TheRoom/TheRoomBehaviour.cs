using System;
using System.Collections.Generic;
using System.IO;

using Assets.Scripts.Constants;
using Assets.Scripts.Core;
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
        private GameState currentGameState;

        public Camera sceneCamera;
        public RoomElementBehaviour selectedElement;
        public RoomElementListBehaviour RoomElementListBehaviour;

        private UnityEngine.Vector3 spawn = new UnityEngine.Vector3(2, 2, 2);
        public RoomType CurrentRoomType { get; private set; }

        public void Awake()
        {
            this.objectsContainer = transform.Find("ObjectsContainer").gameObject;

            this.currentGameState = Base.Core.Game.State;

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

            if (Time.timeScale > 0)
            {
                if (currentGameState.CurrentLevel != default)
                {
                    currentGameState.CurrentLevel.ElapsedTime += Time.deltaTime;
                }
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
                RoomElementToCurrentRoom(selectedElement);

                if (level.IsSelectionRandom)
                {
                    SpawnRandomFromRemainingElement();

                    Base.Core.Game.PlayButtonSound();
                }
                else
                {
                    SetSelectedElement(default);

                    if (currentGameState.CurrentLevel.RemainingElements.Count < 1)
                    {
                        LevelCompleted();
                    }
                    else
                    {
                        Base.Core.Game.PlayButtonSound();
                    }
                }
            }
            else
            {
                GameFrame.Base.Audio.Effects.Play("Error");
            }
        }

        private void RoomElementToCurrentRoom(RoomElementBehaviour roomElementBehaviour)
        {
            RoomElement roomElement = roomElementBehaviour.Element;
            roomElement.Position = roomElementBehaviour.transform.position.ToFrame();
            roomElement.Rotation = roomElementBehaviour.transform.eulerAngles.ToFrame();
            Debug.Log(roomElement);
            currentGameState.CurrentLevel.TheRoom.Elements.Add(roomElement);
        }

        public void MoveSelectedToSpawn()
        {
            if (selectedElement != default)
            {
                selectedElement.transform.position = spawn;
                selectedElement.UpdateIsPlaceable();
            }
        }

        public void SpawnFromKey(String key)
        {
            var roomElementType = currentGameState.GameMode.ElementTypes[key];

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
            var remainingElements = currentGameState.CurrentLevel.RemainingElements;

            if (remainingElements.Count > 0)
            {
                var randomKey = remainingElements.GetRandomKey();

                remainingElements[randomKey]--;

                if (remainingElements[randomKey] < 1)
                {
                    remainingElements.Remove(randomKey);
                }

                SpawnFromKey(randomKey);

                RoomElementListBehaviour.UpdateList();
            }
            else
            {
                LevelCompleted();
            }
        }

        private void LevelCompleted()
        {
            currentGameState.CurrentLevel.IsCompleted = true;
            currentGameState.CompletedLevels.Add(currentGameState.CurrentLevel);
            currentGameState.CurrentLevel = default;

            GameFrame.Base.Audio.Effects.Play("LevelCompleted");
            Base.Core.Game.ChangeScene(SceneNames.World);
        }

        public void OnSlotSelected(RoomElementListSlotBehaviour slot)
        {
            var level = GetCurrentLevelDefinition();

            if (!level.IsSelectionRandom)
            {
                if (selectedElement == default || selectedElement.IsPlaceable)
                {
                    Base.Core.Game.PlayButtonSound();

                    var key = slot.RoomElementItem.Type;

                    SpawnFromKey(key);

                    slot.RoomElementItem.Quantity--;

                    var remainingElements = currentGameState.CurrentLevel.RemainingElements;

                    remainingElements[key]--;

                    if (remainingElements[key] < 1)
                    {
                        remainingElements.Remove(key);
                    }

                    RoomElementListBehaviour.UpdateList();
                }
                else
                {
                    GameFrame.Base.Audio.Effects.Play("Error");
                }
            }
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
                Model = "Wall"
            };
            roomType.WallElements.Add(wall);
            wall.Positions.Add(new GameFrame.Core.Math.Vector3(5, 5, 5));
            //var roof = new WallElement();
            //for (int x = 0; x < sizeX; x++)
            //{
            //    for (int y = 0; y < sizeY; y++)
            //    {
            //        wall.Positions.Add(new GameFrame.Core.Math.Vector3(x, y, 0));
            //        wall.Positions.Add(new GameFrame.Core.Math.Vector3(x, y, sizeZ - 1));
            //    }
            //    for (int z = 0; z < sizeZ; z++)
            //    {
            //        wall.Positions.Add(new GameFrame.Core.Math.Vector3(x, 0, z));
            //        //wall.Positions.Add(new GameFrame.Core.Math.Vector3(x, sizeY - 1, z));
            //    }
            //}
            //for (int y = 0; y < sizeY; y++)
            //{
            //    for (int z = 0; z < sizeZ; z++)
            //    {
            //        wall.Positions.Add(new GameFrame.Core.Math.Vector3(0, y, z));
            //        wall.Positions.Add(new GameFrame.Core.Math.Vector3(sizeX - 1, y, z));
            //    }
            //}
            DumpRoom(roomType);
            foreach (var wallElem in roomType.WallElements)
            {
                availableModels.TryGetValue(wallElem.Model, out var modelTemplate);
                foreach (var position in wallElem.Positions)
                {
                    InstantiateWallElement(position, modelTemplate);
                }
            }
        }

        private void DumpRoom(RoomType room)
        {
            var json = GameFrame.Core.Json.Handler.SerializePretty(room);
            var filePath = Application.streamingAssetsPath + "/room.json";
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.Write(json);
            writer.Close();
        }

        public void SpawnTest()
        {
            var roomElement = new RoomElement("Longbox")
            {
                Model = "Longbox",
                IconReference = "2d_Longbox"
                //Rotatable = roomElementType.Rotatable
            };

            AddRoomElement(roomElement, spawn);
        }

        private void LoadCurrentRoom()
        {
            CurrentRoomType = GetRoomTypeByLevelId();
            spawn = new UnityEngine.Vector3(CurrentRoomType.Size.X / 2, CurrentRoomType.Size.Y + 1, CurrentRoomType.Size.Z / 2);
            //roomPlacements = new Boolean[(int)CurrentRoomType.Size.X, (int)CurrentRoomType.Size.Y, (int)CurrentRoomType.Size.Z];
            foreach (var wallElem in CurrentRoomType.WallElements)
            {
                availableModels.TryGetValue(wallElem.Model, out var modelTemplate);
                foreach (var position in wallElem.Positions)
                {
                    InstantiateWallElement(position, modelTemplate);
                }
            }

            AdjustCamera();

            if (currentGameState.CurrentLevel.TheRoom.Elements == default)
            {
                currentGameState.CurrentLevel.TheRoom.Elements = new List<RoomElement>();
            }

            foreach (var roomElement in currentGameState.CurrentLevel.TheRoom.Elements)
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
            //roomPlacements[(int)position.X, (int)position.Y, (int)position.Z] = true;
        }

        private RoomType GetRoomTypeByLevelId()
        {
            var level = GetCurrentLevelDefinition();

            if (level != null)
            {
                return currentGameState.GameMode.RoomTypes[level.RoomReference];
            }

            return default;
        }

        private LevelDefinition GetCurrentLevelDefinition()
        {
            Guid iD = currentGameState.CurrentLevel.ID;
            currentGameState.GameMode.LevelsByID.TryGetValue(iD, out var level);
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
            AddRoomElement(roomElement, roomElement.Position.ToUnity(), setSelected);
        }

        private void AddRoomElement(RoomElement roomElement, UnityEngine.Vector3 position, Boolean setSelected = true)
        {
            if (roomElement.Model.HasValue() && availableModels.TryGetValue(roomElement.Model, out var modelTemplate))
            {
                var mat = Instantiate(modelTemplate, objectsContainer.transform);

                var roomElementBehaviour = mat.AddRoomElement(roomElement, this);

                if(!string.IsNullOrEmpty(roomElement.Material))
                {
                    var material = GameFrame.Base.Resources.Manager.Materials.Get(roomElement.Material);
                    mat.GetComponent<Renderer>().material = material;
                }

                roomElementBehaviour.HighlightBehaviour.ReloadRenderers();

                mat.transform.position = position;
                mat.transform.eulerAngles = roomElement.Rotation.ToUnity();
                mat.SetActive(true);

                if (setSelected)
                {
                    SetSelectedElement(roomElementBehaviour);
                }
            }
        }

        private void SetSelectedElement(RoomElementBehaviour roomElementBehaviour)
        {
            if (this.selectedElement != null)
            {
                this.selectedElement.SetSelected(false);
            }

            this.selectedElement = roomElementBehaviour;

            if (roomElementBehaviour != null)
            {
                roomElementBehaviour.SetSelected(true);
            }
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