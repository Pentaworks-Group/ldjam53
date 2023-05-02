using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Constants;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Definitions;

using GameFrame.Core.Extensions;

using UnityEngine;

namespace Assets.Scripts.Scenes.World
{
    public class WorldBehaviour : MonoBehaviour
    {
        private GameState gameState;
        
        private GameObject levelTemplate;
        private GameObject levelsContainer;
        private GameObject owlSelection;

        public void OnLevelSelected(LevelBehaviour selectedLevel)
        {
            var level = selectedLevel.Level;

            if (level == default)
            {
                level = GenerateLevel(selectedLevel.LevelDefinition);
            }

            if (level != default)
            {
                Base.Core.Game.PlayButtonSound();

                gameState.CurrentLevel = level;
                Base.Core.Game.ChangeScene(SceneNames.TheRoom);
            }
        }

        public void OnOwlKindSelected(String owlType)
        {
            gameState.SelectedOwlType = owlType;

            this.owlSelection.SetActive(false);

            Base.Core.Game.PlayButtonSound();
        }

        private void LoadLevels()
        {
            if (this.gameState?.GameMode?.Levels?.Count >= 0)
            {
                var isUnlocked = true;

                var currentRotation = 180;

                var anglePerItem = 360 / this.gameState.GameMode.Levels.Count;

                var radius = .5f;
                var buttonHalfSize = .5f / (Mathf.Sqrt(this.gameState.GameMode.Levels.Count)); 

                foreach (var levelDefinition in this.gameState.GameMode.Levels)
                {
                    var levelItem = Instantiate(levelTemplate, levelsContainer.transform);

                    if (levelItem.TryGetComponent<LevelBehaviour>(out var levelBehaviour))
                    {
                        var radian = Mathf.Deg2Rad * currentRotation;

                        var cosX = Mathf.Cos(radian);
                        var sinZ = Mathf.Sin(radian);

                        var x = radius * cosX + .5f;
                        var y = radius * sinZ + .5f;

                        var completedLevel = gameState.CompletedLevels.FirstOrDefault(l => l.ID == levelDefinition.ID);

                        levelBehaviour.SetLevel(levelDefinition, completedLevel, isUnlocked);

                        if (completedLevel == default)
                        {
                            isUnlocked = false;
                        }

                        var anchorMin = new Vector2(x - buttonHalfSize, y - buttonHalfSize);
                        var anchorMax = new Vector2(x + buttonHalfSize, y + buttonHalfSize);

                        currentRotation -= anglePerItem;

                        levelBehaviour.gameObject.SetActive(true);
                        var rect = levelBehaviour.gameObject.GetComponent<RectTransform>();
                        rect.anchorMin = anchorMin;
                        rect.anchorMax = anchorMax;
                        //levelBehaviour.gameObject.transform.Translate(newPosition, Space.Self);

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
                this.levelTemplate = GameObject.Find("UI/Fitter/Container/Templates/LevelTemplate");
                this.levelsContainer = GameObject.Find("UI/Fitter/Container/Levels");
                //this.levelTemplate = GameObject.Find("UI/Container/Templates/LevelTemplate");
                //this.levelsContainer = GameObject.Find("UI/Container/Levels");
                
                LoadLevels();

                if (!gameState.SelectedOwlType.HasValue())
                {
                    this.owlSelection = GameObject.Find("UI/Fitter/OwlSelection");
                    //this.owlSelection = GameObject.Find("UI/OwlSelection");

                    this.owlSelection.SetActive(true);
                }
            }
        }
    }
}
