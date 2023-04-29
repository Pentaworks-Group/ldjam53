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

        public void SetElement(RoomElement roomElement)
        {
            if (roomElement == default)
            {
                throw new ArgumentNullException(nameof(roomElement));
            }

            this.element = roomElement;

            this.name = roomElement.Texture;
            this.highlightBehaviour = this.gameObject.AddComponent<HighlightBehaviour>();
            this.highlightBehaviour.SetElement(roomElement);
        }

        public void SetSelected(Boolean isSelected)
        {
            this.element.Selected = isSelected;
            this.highlightBehaviour.UpdateHightlight();
        }

        private void Start()
        {
        }

        private void Update()
        {
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
                this.element.IsPlaceable = hasCollisions;
            }

            this.highlightBehaviour.UpdateHightlight();
        }
    }
}
