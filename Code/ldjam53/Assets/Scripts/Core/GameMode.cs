using System;
using System.Collections.Generic;
using Assets.Scripts.Models;
using GameFrame.Core.Math;

namespace Assets.Scripts.Core
{
    public class GameMode
    {
        public String Name { get; set; }
        public String Description { get; set; }

        public Room TheRoom { get; set; }
    }
}
