using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Constants;
using Assets.Scripts.Core.Definitions;

using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : GameFrame.Core.Game<GameState, PlayerOptions, SavedGamedPreviewImpl>
    {
        public List<AudioClip> AudioClipListMenu { get; set; }
        public List<AudioClip> AudioClipListGame { get; set; }

        private readonly IList<GameMode> availableGameModes = new List<GameMode>();
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
            if (SelectedGameMode == default)
            {
                SelectedGameMode = AvailableGameModes[0];
            }
            // Maybe add a Tutorial scene, where the user can set "skip" for the next time.
            var gameState = new GameState()
            {
                CurrentScene = SceneNames.TheRoom,
                GameMode = this.SelectedGameMode
            };

            GenerateLevel(gameState);

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

            AudioClipListMenu = new List<AudioClip>()
            {
                GameFrame.Base.Resources.Manager.Audio.Get("Menu_1"),
                GameFrame.Base.Resources.Manager.Audio.Get("Menu_2"),
            };
            AudioClipListGame = new List<AudioClip>()
            {
                GameFrame.Base.Resources.Manager.Audio.Get("Menu_1"),
                GameFrame.Base.Resources.Manager.Audio.Get("Menu_2"),
                GameFrame.Base.Resources.Manager.Audio.Get("Game_1"),
                GameFrame.Base.Resources.Manager.Audio.Get("Game_2"),
                GameFrame.Base.Resources.Manager.Audio.Get("Game_4"),
                GameFrame.Base.Resources.Manager.Audio.Get("Game_5"),
                GameFrame.Base.Resources.Manager.Audio.Get("Game_6"),
            };

            GameFrame.Base.Audio.Background.Play(AudioClipListMenu);
        }

        private void GenerateLevel(GameState gameState)
        {
            if (gameState.GameMode.Levels.Count > 0)
            {
                var levelDefinition = gameState.GameMode.Levels.FirstOrDefault();

                var level = new Models.Level()
                {
                    ID = levelDefinition.ID,
                    RemainingElements = GenerateRemainingElements(levelDefinition),
                    TheRoom = new Models.Room()
                };

                gameState.CurrentLevel = level;
            }
            else
            {
                throw new System.Exception("Failed to generate Level from LevelDefinition!");
            }
        }

        private Dictionary<String, Int32> GenerateRemainingElements(LevelDefinition levelDefinition)
        {
            var remainingElements = new Dictionary<String, Int32>();

            foreach (var item in levelDefinition.Elements)
            {
                remainingElements[item.Key] = item.Value;
            }

            return remainingElements;
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