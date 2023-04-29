using Assets.Scripts.Constants;

using UnityEngine;

namespace Assets.Scripts.Scenes.Menues
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        public void ShowSavedGames()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.SavedGames);
        }

        public void ShowModes()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.GameMode);
        }

        public void ShowOptions()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.Options);
        }

        public void ShowCredits()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.Credits);
        }

        public void PlayGame()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.Credits);
        }

        public void GoToRoom()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.TheRoom);
        }
    }
}
