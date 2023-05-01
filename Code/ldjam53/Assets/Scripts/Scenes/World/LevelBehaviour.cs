using System;

using Assets.Scripts.Core.Definitions;
using Assets.Scripts.Models;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.World
{
    public class LevelBehaviour : MonoBehaviour
    {
        private TMP_Text nameText;
        private Image completedImage;
        private Button button;

        private Boolean isUnlocked;
        public Boolean IsUnlocked
        {
            get
            {
                return this.isUnlocked;
            }
        }

        private LevelDefinition levelDefinition;
        public LevelDefinition LevelDefinition
        {
            get
            {
                return this.levelDefinition;
            }
        }

        private Level level;
        public Level Level
        {
            get
            {
                return this.level;
            }
        }

        public void SetLevel(LevelDefinition levelDefinition, Level level, Boolean isUnlocked)
        {
            this.levelDefinition = levelDefinition;
            this.level = level;
            this.isUnlocked = isUnlocked;
        }

        public void UpdateUI()
        {
            this.nameText.text = this.levelDefinition.Name;

            if (this.level != default)
            {
                this.completedImage.gameObject.SetActive(true);
            }
            else
            {
                this.completedImage.gameObject.SetActive(false);
            }

            if (this.isUnlocked)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }

        private void Awake()
        {
            TryGetComponent(out this.button);
            transform.Find("ImageArea/NameText").TryGetComponent(out this.nameText);
            transform.Find("ImageArea/CompletedImage").TryGetComponent(out this.completedImage);
        }
    }
}
