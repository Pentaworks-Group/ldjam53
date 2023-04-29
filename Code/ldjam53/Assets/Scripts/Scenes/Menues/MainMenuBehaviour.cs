using Assets.Scripts.Base;
using Assets.Scripts.Constants;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    public void ShowSavedGames()
    {
        Core.Game.PlayButtonSound();
        Core.Game.ChangeScene(SceneNames.SavedGames);
    }

    public void ShowModes()
    {
        Core.Game.PlayButtonSound();
        Core.Game.ChangeScene(SceneNames.GameMode);
    }

    public void ShowOptions()
    {
        Core.Game.PlayButtonSound();
        Core.Game.ChangeScene(SceneNames.Options);
    }

    public void ShowCredits()
    {
        Core.Game.PlayButtonSound();
        Core.Game.ChangeScene(SceneNames.Credits);
    }

    public void PlayGame()
    {
        Core.Game.PlayButtonSound();
        Core.Game.ChangeScene(SceneNames.Credits);
    }

}
