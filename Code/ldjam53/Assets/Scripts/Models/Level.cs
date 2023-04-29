using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class Level
    {
        public Room Room { get; set; }
        public List<RoomElement> Elements { get; set; }
    }
}
