using Assets.Scripts.Models;
using System;
using System.Collections.Generic;


namespace Assets.Scripts.Core
{
    public class GameState : GameFrame.Core.GameState
    {
        public GameMode GameMode { get; set; }

        public float ElapsedTime { get; set; }

        public Room TheRoom { get; set; }
    }
}
