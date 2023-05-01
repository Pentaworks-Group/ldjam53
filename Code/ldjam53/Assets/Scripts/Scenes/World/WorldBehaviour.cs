using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Constants;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Definitions;

using UnityEngine;

namespace Assets.Scripts.Scenes.World
{
    public class WorldBehaviour : MonoBehaviour
    {
        private GameState gameState;
        private GameObject levelTemplate;
        private GameObject levelsContainer;

        public void OnLevelSelected(LevelBehaviour selectedLevel)
        {
            var level = selectedLevel.Level;

            if (level == default)
            {
                level = GenerateLevel(selectedLevel.LevelDefinition);
            }

            if (level != default)
            {
                gameState.CurrentLevel = level;
                Base.Core.Game.ChangeScene(SceneNames.TheRoom);
            }
        }

        private void LoadLevels()
        {
            if (this.gameState?.GameMode?.Levels?.Count >= 0)
            {
                var isUnlocked = true;

                var currentRotation = 0;

                var anglePerItem = 360 / this.gameState.GameMode.Levels.Count;

                var radius = -400f;

                foreach (var levelDefinition in this.gameState.GameMode.Levels)
                {
                    var levelItem = Instantiate(levelTemplate, levelsContainer.transform);

                    if (levelItem.TryGetComponent<LevelBehaviour>(out var levelBehaviour))
                    {
                        var radian = Mathf.Deg2Rad * currentRotation;

                        var cosX = Mathf.Cos(radian);
                        var sinZ = Mathf.Sin(radian);

                        var x = radius * cosX;
                        var y = radius * sinZ;

                        var completedLevel = gameState.CompletedLevels.FirstOrDefault(l => l.ID == levelDefinition.ID);

                        levelBehaviour.SetLevel(levelDefinition, completedLevel, isUnlocked);

                        if (completedLevel == default)
                        {
                            isUnlocked = false;
                        }

                        var newPosition = new Vector3(x, y, 0);

                        currentRotation -= anglePerItem;

                        levelBehaviour.gameObject.SetActive(true);
                        levelBehaviour.gameObject.transform.Translate(newPosition, Space.Self);

                        levelBehaviour.UpdateUI();
                    }
                }
            }
        }


        private Models.Level GenerateLevel(LevelDefinition levelDefinition)
        {
            if (levelDefinition != default)
            {
                var level = new Models.Level()
                {
                    ID = levelDefinition.ID,
                    RemainingElements = new Dictionary<String, Int32>(levelDefinition.Elements),
                    TheRoom = new Models.Room()
                };

                return level;
            }

            return default;
        }

        private void Awake()
        {
            this.gameState = Base.Core.Game.State;

            if (this.gameState != default)
            {
                this.levelTemplate = GameObject.Find("UI/Container/Templates/LevelTemplate");
                this.levelsContainer = GameObject.Find("UI/Container/Levels");

                LoadLevels();
            }
        }
    }
}
