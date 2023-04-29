using Assets.Scripts.Models;
using Assets.Scripts.Scenes.TheRoom;

namespace Assets.Scripts.Extensions
{
    public static class GameObjectExtensions
    {
        public static RoomElementBehaviour AddRoomElement(this UnityEngine.GameObject gameObject, RoomElement roomElement)
        {
            if ((gameObject != null) && (roomElement != default))
            {
                var behaviour = gameObject.AddComponent<RoomElementBehaviour>();

                behaviour.SetElement(roomElement);

                return behaviour;
            }

            return null;
        }
    }
}
