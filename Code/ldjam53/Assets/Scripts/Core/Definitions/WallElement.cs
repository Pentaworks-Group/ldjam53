using System;
using System.Collections.Generic;

using GameFrame.Core.Math;

namespace Assets.Scripts.Core.Definitions
{
    public class WallElement
    {
        public String Model { get; set; }
        public String Material { get; set; }
        public List<Vector3> Positions { get; set; }
    }
}
