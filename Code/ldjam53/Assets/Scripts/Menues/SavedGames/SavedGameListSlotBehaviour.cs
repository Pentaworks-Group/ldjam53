using System.Collections.Generic;

using Assets.Scripts.Core;

using GameFrame.Core.UI.List;

using UnityEngine.UI;

namespace Assets.Scripts.Scene.SaveGame
{
    public class SavedGameListSlotBehaviour : ListSlotBehaviour<KeyValuePair<string, SavedGamedPreviewImpl>>
    {
        private Text createdOn;
        private Text timeStamp;
        private Text timeElapsed;

        public override void RudeAwake()
        {
            createdOn = transform.Find("SlotContainer/Info/Created").GetComponent<Text>();
            timeStamp = transform.Find("SlotContainer/Info/TimeStamp").GetComponent<Text>();
            timeElapsed = transform.Find("SlotContainer/Info/TimeElapsed").GetComponent<Text>();
        }

        public SavedGamedPreviewImpl GetSavedGamedPreview()
        {
            return content.Value;
        }

        public void DisplaySlot(SavedGameDetailBehaviour details)
        {
            details.DisplayDetails(GetSavedGamedPreview());
        }

        public override void UpdateUI()
        {
            SavedGamedPreviewImpl savedGame = GetSavedGamedPreview();

            createdOn.text = savedGame.CreatedOn;
            timeStamp.text = savedGame.SavedOn;
            timeElapsed.text = savedGame.TimeElapsed;
        }

        public void LoadGame()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.LoadSavedGame(content.Key);
        }
    }
}
