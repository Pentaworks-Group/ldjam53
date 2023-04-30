using Assets.Scripts.Models;
using Assets.Scripts.Scenes.TheRoom;

namespace Assets.Scripts.Extensions
{
    public static class GameObjectExtensions
    {
        public static RoomElementBehaviour AddRoomElement(this UnityEngine.GameObject gameObject, RoomElement roomElement, TheRoomBehaviour theRoomBehaviour)
        {
            if ((gameObject != null) && (roomElement != default))
            {
                var behaviour = gameObject.AddComponent<RoomElementBehaviour>();

                behaviour.SetElement(roomElement, theRoomBehaviour);

                return behaviour;
            }

            return null;
        }
    }
}
