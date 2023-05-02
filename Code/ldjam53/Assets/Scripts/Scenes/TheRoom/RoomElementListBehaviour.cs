using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Extensions;

using GameFrame.Core.UI.List;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementListBehaviour : ListContainerBehaviour<RoomElementListItem>
    {
        public TheRoomBehaviour theRoomBehaviour;

        public override void CustomStart()
        {
            UpdateList();
        }

        public void UpdateList()
        {
            var items = new Dictionary<String, RoomElementListItem>();

            if (theRoomBehaviour.RemainingElements?.Count > 0)
            {
                foreach (var item in theRoomBehaviour.RemainingElements)
                {
                    if (!items.TryGetValue(item.Key, out var listItem))
                    {
                        var elementType = item.Key.GetElementType();

                        listItem = new RoomElementListItem()
                        {
                            Type = item.Key,
                            Name = elementType.Name,
                            IconReference = elementType.IconReference,
                            Quantity = item.Value,
                        };

                        items[item.Key] = listItem;
                    }
                    else
                    {
                        listItem.Quantity++;
                    }
                }
            }

            SetContentList(items.Values.ToList());
        }
    }
}