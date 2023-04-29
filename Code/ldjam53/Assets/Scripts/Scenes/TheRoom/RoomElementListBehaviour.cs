using System.Collections.Generic;

using Assets.Scripts.Models;

using GameFrame.Core.UI.List;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementListBehaviour : ListContainerBehaviour<RoomElement>
    {
        public override void CustomStart()
        {
            UpdateList();
        }

        public void UpdateList()
        {
            var temp = new List<RoomElement>()
            {
                new RoomElement("Box")
                {
                    Model = "Pack_box_edge",
                    Rotatable = true,
                    Rotation = GameFrame.Core.Math.Vector3.Zero
                },
                new RoomElement("LongBox")
                {
                    Model = "Pack_box_long_edge",
                    Rotatable = true,
                    Rotation = GameFrame.Core.Math.Vector3.Zero
                },
                new RoomElement("Broom")
                {
                    Model = "Broom_edge",
                    Rotatable = true,
                    Rotation = GameFrame.Core.Math.Vector3.Zero
                }
            };

            SetContentList(temp);
        }

        public void OnSlotSelected(RoomElementListSlotBehaviour slot)
        {

        }
    }
}