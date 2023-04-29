using Assets.Scripts.Constants;
using UnityEngine;
using Assets.Scripts.Models;

namespace Assets.Scripts.Scenes
{
    public class TheRoomBehaviour : MonoBehaviour
    {
        public GameObject template;

        private float scale;

        public void ToMainMenu()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.MainMenu);
        }


        public void Awake()
        {
            RectTransform rt = (RectTransform)template.transform.transform;
            scale = rt.rect.width;
        }


        public void TestLoad()
        {
            Room room = new Room();
            room.Materials = new RoomMaterial[20, 20, 20];
            var ix = room.Materials.GetLength(0) - 1;
            for (int x = 0; x < room.Materials.GetLength(0); x++)
            {
                for (int z = 0; z < room.Materials.GetLength(2); z++)
                {
                    room.Materials[x, 0, z] = new RoomMaterial("floor");
                }
                for (int y = 0; y < room.Materials.GetLength(1); y++)
                {
                    room.Materials[x, y, ix] = new RoomMaterial("wall xy");
                }
            }
            
            for (int z = 0; z < room.Materials.GetLength(2); z++)
            {
                for (int y = 0; y < room.Materials.GetLength(1); y++)
                {
                    room.Materials[ix, y, z] = new RoomMaterial("wall zy");
                }
            }
            LoadRoom(room);
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
    }
}