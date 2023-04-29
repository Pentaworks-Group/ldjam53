using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Definitions
{
    public class ElementType
    {
        public String Name { get; set; }
        public String IconReference { get; set; }
        public List<String> Models { get; set; }
        public List<String> Materials { get; set; }
    }
}
