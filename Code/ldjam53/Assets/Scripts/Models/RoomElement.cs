using System;

using GameFrame.Core.Math;

namespace Assets.Scripts.Models
{
    public class RoomElement
    {
        public RoomElement(String texture)
        {
            this.Texture = texture;
        }

        public String Texture { get; set; }
        public String Model { get; set; }
        public Vector3 Rotation { get; set; }
        public Boolean Rotatable { get; set; } = true;
        public Boolean Selected { get; set; }
        public Boolean IsPlaceable { get; set; } = true;
        public Boolean IsPlaced { get; set; }
        public Int32 CollisionAmount { get; set; }





        public String IconReference { get; set; }
    }
}
