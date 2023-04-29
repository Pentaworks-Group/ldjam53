using System;
using System.Collections.Generic;

using Assets.Scripts.Core.Definitions;
using Assets.Scripts.Models;

namespace Assets.Scripts.Core
{
    public class GameMode
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public Dictionary<String, RoomType> RoomTypes { get; set; }
        public Dictionary<String, ElementType> ElementTypes { get; set; }
        public Dictionary<Guid, LevelDefinition> Levels { get; set; }
    }
}
