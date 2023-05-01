using System;

using Assets.Scripts.Models;

using UnityEngine;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementBehaviour : MonoBehaviour
    {
        private HighlightBehaviour highlightBehaviour;
        private TheRoomBehaviour theRoomBehaviour;

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

        public HighlightBehaviour HighlightBehaviour
        {
            get
            {
                return highlightBehaviour;
            }
        }

        public void SetElement(RoomElement roomElement, TheRoomBehaviour theRoomBehaviour)
        {
            if (roomElement == default)
            {
                throw new ArgumentNullException(nameof(roomElement));
            }

            this.theRoomBehaviour = theRoomBehaviour;

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

        private bool IsInBound()
        {
            if (transform.position.x < 0 || transform.position.x > theRoomBehaviour.CurrentRoomType.Size.X - 1)
            {
                return false;
            }
            if (transform.position.y < 0 || transform.position.y > theRoomBehaviour.CurrentRoomType.Size.Y - 1)
            {
                return false;
            }
            if (transform.position.z < 0 || transform.position.z > theRoomBehaviour.CurrentRoomType.Size.Z - 1)
            {
                return false;
            }
            return true;
        }

        public void UpdateIsPlaceable()
        {
            var hasNoCollisions = this.element.CollisionAmount < 1;

            if (this.element.Selected)
            {

                this.isPlaceable = hasNoCollisions && IsInBound(); // && theRoomBehaviour.IsInEmptySpace(transform.position)
            }

            this.highlightBehaviour.UpdateHightlight();
        }
    }
}
