using System;
using System.Collections.Generic;

using Assets.Scripts.Constants;

using GameFrame.Core.Audio.Continuous;
using GameFrame.Core.Audio.Multi;

using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : GameFrame.Core.Game<GameState, PlayerOptions, SavedGamedPreviewImpl>
    {
        public IList<GameMode> AvailableGameModes { get; } = new List<GameMode>();
        public GameMode SelectedGameMode { get; set; }

        public List<AudioClip> AudioClipListMenu { get; set; }
        public List<AudioClip> AudioClipListGame { get; set; }
        public List<AudioClip> AmbientClipList { get; set; }
        public List<String> AmbientEffectsClipList { get; set; }

        public bool LockCameraMovement { get; set; } = false;

        public void PlayButtonSound()
        {
            GameFrame.Base.Audio.Effects.Play("Button");
        }

        protected override GameState InitializeGameState()
        {
            // Maybe add a Tutorial scene, where the user can set "skip" for the next time.
            var gameState = new GameState()
            {
                CurrentScene = SceneNames.World,
                GameMode = this.SelectedGameMode
            };

            GenerateWorld(gameState);

            return gameState;
        }

        protected override PlayerOptions InitialzePlayerOptions()
        {
            return new PlayerOptions()
            {
                AreAnimationsEnabled = true,
                EffectsVolume = 0.7f,
                AmbienceVolume = 0.3f,
                BackgroundVolume = 0.25f,
                IsMouseScreenEdgeScrollingEnabled = true,
                MoveSensivity = 0.5f,
                ZoomSensivity = 0.5f
            };
        }

        protected override void OnGameStart()
        {
            base.OnGameStart();

            var backgroundClips = new List<AudioClip>()
            {
                GameFrame.Base.Resources.Manager.Audio.Get("Background_Music_1"),
            };

            GameFrame.Base.Audio.Background.Play(backgroundClips);
        }

        private void GenerateWorld(GameState gameState)
        {

        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void GameStart()
        {
            Base.Core.Game.Startup();
        }
    }
}