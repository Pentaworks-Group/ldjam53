using System;

namespace Assets.Scripts.Core
{
    public class PlayerOptions : GameFrame.Core.PlayerOptions
    {
        public Boolean IsMouseScreenEdgeScrollingEnabled { get; set; } = false;

        public float MoveSensivity { get; set; } = 0.5f;
        public float LookSensivity { get; set; } = 10f;
        public float ZoomSensivity { get; set; } = 0.5f;
    }
}
