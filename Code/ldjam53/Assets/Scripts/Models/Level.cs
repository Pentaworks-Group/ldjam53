using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class Level
    {
        public Guid ID { get; set; }
        public Dictionary<String, Int32> RemainingElements { get; set; }
        public Room TheRoom { get; set; }
    }
}
