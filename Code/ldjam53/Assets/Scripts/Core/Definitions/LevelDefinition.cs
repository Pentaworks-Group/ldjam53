using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Definitions
{
    public class LevelDefinition
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
        public String RoomReference { get; set; }
        public Boolean IsSelectionRandom { get; set; }
        public Dictionary<String, Int32> Elements { get; set; }
    }
}
