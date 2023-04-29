using Assets.Scripts.Models;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public class GameState : GameFrame.Core.GameState
    {
        public float ElapsedTime { get; set; }
        public GameMode GameMode { get; set; }
        public List<Level> Levels { get; set; }
        public Level CurrentLevel { get; set; }
    }
}
