using System;

using GameFrame.Core.Math;

namespace Assets.Scripts.Models
{
    public class RoomElement
    {
        public RoomElement(String name)
        {
            this.Name = name;
        }

        public String Name { get; set; }
        public String Material { get; set; }
        public String Model { get; set; }
        public String IconReference { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Boolean Rotatable { get; set; } = true;
        public Boolean Selected { get; set; }
        public Int32 CollisionAmount { get; set; }
    }
}
