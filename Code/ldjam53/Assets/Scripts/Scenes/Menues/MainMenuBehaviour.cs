using Assets.Scripts.Constants;

using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

namespace Assets.Scripts.Scenes.Menues
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void Quit();


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
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Quit();
#elif UNITY_STANDALONE
            Application.Quit();
            
#endif
        }

        private void Awake()
        {
            GameObject.Find("UI/Fitter/VersionText").GetComponent<TMPro.TMP_Text>().text = $"Version: {Application.version}";

            //if (GameObject.Find("UI/Fitter/Quit").TryGetComponent(out Button quitButton))
            //{
            //    quitButton.interactable = Base.Core.Game.IsFileAccessPossible;
            //}

            GameFrame.Base.Audio.Background.ReplaceClips(Base.Core.Game.AudioClipListMenu);
        }
    }
}
