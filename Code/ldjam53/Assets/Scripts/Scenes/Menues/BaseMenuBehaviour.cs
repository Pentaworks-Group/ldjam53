using Assets.Scripts.Constants;

using UnityEngine;

namespace Assets.Scripts.Scenes.Menues
{
    public class BaseMenuBehaviour : MonoBehaviour
    {

   
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToMainMenu();
            }

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }    

        public void ToMainMenu()
        {
            Base.Core.Game.PlayButtonSound();
            Base.Core.Game.ChangeScene(SceneNames.MainMenu);
        }
    }
}
