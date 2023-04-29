using System;
using System.Collections.Generic;

using Assets.Scripts.Core.Definitions;
using Assets.Scripts.Models;

using Newtonsoft.Json;

namespace Assets.Scripts.Core
{
    public class GameMode
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public Dictionary<String, RoomType> RoomTypes { get; set; }
        public Dictionary<String, ElementType> ElementTypes { get; set; }
        public List<LevelDefinition> Levels { get; set; }

        private Dictionary<Guid, LevelDefinition> levelsByID;
        [JsonIgnore]
        public Dictionary<Guid, LevelDefinition> LevelsByID 
        {
            get
            {
                if ((levelsByID == default) && (Levels?.Count > 0))
                {
                    levelsByID = new Dictionary<Guid, LevelDefinition>();

                    foreach (var item in Levels)
                    {
                        levelsByID[item.ID] = item;
                    }
                }

                return levelsByID;
            }
        }
    }
}
