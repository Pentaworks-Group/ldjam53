using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class RoomMaterial
    {
        public RoomMaterial(string texture)
        {
            this.texture = texture;
        }

        public string texture;
        private string model;
        private string rotation;
        private bool rotatable = true;
    }
}
