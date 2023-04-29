using System;
using System.Collections.Generic;

using GameFrame.Core.Math;

namespace Assets.Scripts.Core.Definitions
{
    public class RoomType
    {
        public String Name { get; set; }
        public Vector3 Size { get; set; }
        public List<WallElement> WallElements { get; set; }
    }
}
