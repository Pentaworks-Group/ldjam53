using System;

using Assets.Scripts.Models;

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

        private Boolean isPlaceable;
        public Boolean IsPlaceable
        {
            get
            {
                return this.isPlaceable;
            }
        }

        public void SetElement(RoomElement roomElement)
        {
            if (roomElement == default)
            {
                throw new ArgumentNullException(nameof(roomElement));
            }

            this.element = roomElement;

            this.name = roomElement.Name;
            this.highlightBehaviour = this.gameObject.AddComponent<HighlightBehaviour>();
            this.highlightBehaviour.SetElement(this);
        }

        public void SetSelected(Boolean isSelected)
        {
            this.element.Selected = isSelected;
            this.highlightBehaviour.UpdateHightlight();
        }

        private void OnTriggerEnter(Collider other)
        {
            this.element.CollisionAmount++;

            UpdateIsPlaceable();
        }

        private void OnTriggerExit(Collider other)
        {
            this.element.CollisionAmount--;

            UpdateIsPlaceable();
        }

        private void UpdateIsPlaceable()
        {
            var hasCollisions = this.element.CollisionAmount < 1;

            if (this.element.Selected)
            {
                this.isPlaceable = hasCollisions;
            }

            this.highlightBehaviour.UpdateHightlight();
        }
    }
}
