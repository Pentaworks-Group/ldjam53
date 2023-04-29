using System;
using GameFrame.Core.Math;

namespace Assets.Scripts.Models
{
    public class RoomElement
    {
        public RoomElement(string texture)
        {
            this.Texture = texture;
        }

        public string Texture { get; set; }
        public string Model { get; set; }
        public Vector3 Rotation { get; set; }
        public bool Rotatable { get; set; } = true;
        public bool Selected { get; set; }
    }
}
