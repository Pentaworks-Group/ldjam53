using System;
using System.Collections.Generic;

using Assets.Scripts.Core.Definitions;
using Assets.Scripts.Models;

using UnityEngine;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementBehaviour : MonoBehaviour
    {
        private HighlightBehaviour highlightBehaviour;
        private TheRoomBehaviour theRoomBehaviour;

        private Renderer renderer;
        public bool DisableChecks { get; set; } = false;

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

        public void Awake()
        {
            renderer = transform.GetComponent<Renderer>();
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

        //private bool IsInBound()
        //{
        //    if (transform.position.x < 0 || transform.position.x > theRoomBehaviour.CurrentRoomType.Size.X - 1)
        //    {
        //        return false;
        //    }
        //    if (transform.position.y < 0 || transform.position.y > theRoomBehaviour.CurrentRoomType.Size.Y - 1)
        //    {
        //        return false;
        //    }
        //    if (transform.position.z < 0 || transform.position.z > theRoomBehaviour.CurrentRoomType.Size.Z - 1)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        private bool IsInBound()
        {
            var center = renderer.bounds.center;
            center = new Vector3(Mathf.RoundToInt(center.x), Mathf.RoundToInt(center.y), Mathf.RoundToInt(center.z) );
            List<GameFrame.Core.Math.Vector3> xList = new List<GameFrame.Core.Math.Vector3>();
            List<GameFrame.Core.Math.Vector3> yList = new List<GameFrame.Core.Math.Vector3>();
            List<GameFrame.Core.Math.Vector3> zList = new List<GameFrame.Core.Math.Vector3>();
            foreach (WallElement wallElement in theRoomBehaviour.CurrentRoomType.WallElements)
            {
                foreach (GameFrame.Core.Math.Vector3 pos in wallElement.Positions)
                {
                    if (pos.Y == center.y && pos.Z == center.z)
                    {
                        xList.Add(pos);
                    }
                    if (pos.X == center.x && pos.Z == center.z)
                    {
                        yList.Add(pos);
                    }
                    if (pos.Y == center.y && pos.X == center.x)
                    {
                        zList.Add(pos);
                    }
                }
            }
            if (xList.Count > 1)
            {
                bool low = false;
                bool high = false;
                foreach (var pos in xList)
                {
                    if (pos.X < center.x)
                    {
                        low = true;
                    } else if (pos.X > center.x)
                    {
                        high = true;
                    }
                }
                if (low && high)
                {
                    return true;
                }
            }

            if (yList.Count > 1)
            {
                bool low = false;
                bool high = false;
                foreach (var pos in yList)
                {
                    if (pos.Y < center.y)
                    {
                        low = true;
                    }
                    else if (pos.Y > center.y)
                    {
                        high = true;
                    }
                }
                if (low && high)
                {
                    return true;
                }
            }

            if (zList.Count > 1)
            {
                bool low = false;
                bool high = false;
                foreach (var pos in zList)
                {
                    if (pos.Z < center.z)
                    {
                        low = true;
                    }
                    else if (pos.Z > center.z)
                    {
                        high = true;
                    }
                }
                if (low && high)
                {
                    return true;
                }
            }

            return false;
        }


        public void UpdateIsPlaceable()
        {
            if (!DisableChecks)
            {
                var hasNoCollisions = this.element.CollisionAmount < 1;

                if (this.element.Selected)
                {
                    bool inBound = IsInBound();
                    Debug.Log("In bound: " + inBound);
                    this.isPlaceable = hasNoCollisions && inBound; // && theRoomBehaviour.IsInEmptySpace(transform.position)
                }

                this.highlightBehaviour.UpdateHightlight();
            } else
            {
                this.isPlaceable = true;
            }
        }
    }
}
