using System;
using GameFrame.Core.Math;

namespace Assets.Scripts.Models
{
    public class RoomMaterial
    {
        public RoomMaterial(string texture)
        {
            this.texture = texture;
        }

        public string texture { get; set; }
        public string model { get; set; }
        public Vector3 rotation { get; set; }
        public bool rotatable { get; set; } = true;
}
}
