using Assets.Scripts.Constants;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scenes.Menues
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        public void ShowSavedGames()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.SavedGames);
        }

        public void ShowOptions()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.Options);
        }

        public void ShowCredits()
        {
            // No Button sound required as page plays a sound for itself
            Base.Core.Game.ChangeScene(SceneNames.Credits);
        }

        public void PlayGame()
        {
            GameFrame.Base.Audio.Background.ReplaceClips(Base.Core.Game.AudioClipListGame);
            Base.Core.Game.PlayButtonSound();
         
            Base.Core.Game.Start();
        }

        public void ShowModes()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.GameMode);
        }

        public void GoToRoom()
        {
            GameFrame.Base.Audio.Background.ReplaceClips(Base.Core.Game.AudioClipListGame);

            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.TheRoom);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void Awake()
        {
            if (GameObject.Find("UI/Fitter/Quit").TryGetComponent(out Button quitButton))
            {
                quitButton.interactable = Base.Core.Game.IsFileAccessPossible;
            }

            GameFrame.Base.Audio.Background.ReplaceClips(Base.Core.Game.AudioClipListMenu);
        }
    }
}
