using System.Collections.Generic;

using Assets.Scripts.Constants;

using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : GameFrame.Core.Game<GameState, PlayerOptions, SavedGamedPreviewImpl>
    {

        private IList<GameMode> availableGameModes = new List<GameMode>();
        public IList<GameMode> AvailableGameModes
        {
            get
            {
                if (availableGameModes.Count == 0)
                {
                    LoadGameSettings();
                }
                return availableGameModes;
            }
        }
        public GameMode SelectedGameMode { get; set; }

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

            //var backgroundClips = new List<AudioClip>()
            //{
            //    GameFrame.Base.Resources.Manager.Audio.Get("Background_Music_1"),
            //};

            //GameFrame.Base.Audio.Background.Play(backgroundClips);
        }

        private void GenerateWorld(GameState gameState)
        {

        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void GameStart()
        {
            Base.Core.Game.Startup();
        }



        public void LoadGameSettings()
        {
            var filePath = Application.streamingAssetsPath + "/GameModes.json";
            var gameO = new GameObject();
            var mono = gameO.AddComponent<Scenes.Menues.BaseMenuBehaviour>();
            mono.StartCoroutine(GameFrame.Core.Json.Handler.DeserializeObjectFromStreamingAssets<List<GameMode>>(filePath, SetGameSettings));
            GameObject.Destroy(gameO);
        }

        private List<GameMode> SetGameSettings(List<GameMode> loadedGameModes)
        {
            if (loadedGameModes?.Count > 0)
            {
                foreach (var gameMode in loadedGameModes)
                {
                    availableGameModes.Add(gameMode);
                }
            }

            if (SelectedGameMode == default)
            {
                SelectedGameMode = loadedGameModes[0];
            }

            return loadedGameModes;
        }
    }
}