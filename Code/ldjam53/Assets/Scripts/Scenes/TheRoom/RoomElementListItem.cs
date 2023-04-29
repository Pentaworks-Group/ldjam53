using System;
using System.Collections.Generic;

using Assets.Scripts.Models;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementListItem
    {
        public String Name { get; set; }
        public String Model { get; set; }
        public Int32 Quantity { get; set; }
        public List<RoomElement> Elements { get; } = new List<RoomElement>();
    }
}
