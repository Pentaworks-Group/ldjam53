using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Scripts.Models;

using Unity.VisualScripting;

using UnityEngine;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementBehaviour : MonoBehaviour
    {
        private HighlightBehaviour highlightBehaviour;
        
        private RoomElement element;
        public RoomElement Element
        {
            get
            {
                return this.element;
            }
        }

        public void SetElement(RoomElement roomElement)
        {
            if (roomElement == default)
            {
                throw new ArgumentNullException(nameof(roomElement));
            }

            this.element = roomElement;

            this.name = roomElement.Texture;
            this.highlightBehaviour = this.gameObject.AddComponent<HighlightBehaviour>();
        }

        public void SetSelected(Boolean isSelected)
        {
            this.element.Selected = isSelected;
            this.highlightBehaviour.ToggleHighlight(isSelected);
        }

        private void Start()
        {
        }

        private void Update()
        {
            
        }
    }
}
