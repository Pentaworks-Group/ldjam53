using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Models;

using GameFrame.Core.UI.List;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementListBehaviour : ListContainerBehaviour<RoomElementListItem>
    {
        public override void CustomStart()
        {
            UpdateList();
        }

        public void UpdateList()
        {
            var items = new List<RoomElement>();

            if (Base.Core.Game?.State?.CurrentLevel?.Elements?.Count > 0)
            {
                items.AddRange(Base.Core.Game.State.CurrentLevel.Elements);
            }
            else
            {
                items.Add(new RoomElement("Box")
                {
                    Model = "Pack_box_edge",
                    Rotatable = true,
                    Rotation = GameFrame.Core.Math.Vector3.Zero,
                });
                
                items.Add(new RoomElement("Box 2")
                {
                    Model = "Pack_box_edge",
                    Rotatable = true,
                    Rotation = GameFrame.Core.Math.Vector3.Zero,
                });
                
                items.Add(new RoomElement("Box")
                {
                    Model = "Pack_box_edge",
                    Rotatable = true,
                    Rotation = GameFrame.Core.Math.Vector3.Zero,
                });
            }

            var groupedItems = new Dictionary<String, RoomElementListItem>();

            foreach (var item in items)
            {
                if (!groupedItems.TryGetValue(item.Texture, out var listItem))
                {
                    listItem = new RoomElementListItem()
                    {
                        Name = item.Texture,
                        Model = item.Model,
                        Quantity = 1,
                        Elements =
                        {
                            item
                        }
                    };

                    groupedItems[listItem.Name] = listItem;
                }
                else
                {
                    listItem.Quantity++;
                    listItem.Elements.Add(item);
                }
            }

            SetContentList(groupedItems.Values.ToList());
        }

        public void OnSlotSelected(RoomElementListSlotBehaviour slot)
        {
            Base.Core.Game.PlayButtonSound();
            
            // Do Stuff with slot.. e.g. Spawn & Select.
        }
    }
}