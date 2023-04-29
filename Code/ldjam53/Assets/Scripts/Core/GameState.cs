using Assets.Scripts.Models;

namespace Assets.Scripts.Core
{
    public class GameState : GameFrame.Core.GameState
    {
        public float ElapsedTime { get; set; }
        public GameMode GameMode { get; set; }



        public Level CurrentLevel { get; set; }
    }
}
