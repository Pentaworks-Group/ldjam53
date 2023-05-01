using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public class InputPadBehaviour : MonoBehaviour
    {
        public Image MoveSelectedImage;
        public Image TurnSelectedImage;
        public Image MoveCamImage;
        public Image TurnCamImage;

        public CameraBehaviour cameraBehaviour;
        public MoveSelectedBehaviour moveSelectedBehaviour;
        public TurnSelectedBehaviour turnSelectedBehaviour;
        public MoveCamBehaviour moveCamBehaviour;
        public TurnCamBehaviour turnCamBehaviour;

        private Image selectedImage;

        public void Update()
        {
            ShortkeyForMenues();
        }

        private void ShortkeyForMenues()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnMoveSelectedImagePressed();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnTurnSelectedImagePressed();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnMoveCamImagePressed();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                OnTurnCamImagePressed();
            }
        }

        public void OnMoveSelectedImagePressed()
        {
            DeselectIfAlreadySelected(MoveSelectedImage, moveSelectedBehaviour);
        }

        public void OnTurnSelectedImagePressed()
        {
            DeselectIfAlreadySelected(TurnSelectedImage, turnSelectedBehaviour);
        }

        public void OnMoveCamImagePressed()
        {
            DeselectIfAlreadySelected(MoveCamImage, moveCamBehaviour);
        }

        public void OnTurnCamImagePressed()
        {
            DeselectIfAlreadySelected(TurnCamImage, turnCamBehaviour);
        }


        private void DeselectIfAlreadySelected(Image Image, Behaviour selectedBehaviour)
        {
            moveSelectedBehaviour.enabled = false;
            turnSelectedBehaviour.enabled = false;
            moveCamBehaviour.enabled = false;
            turnCamBehaviour.enabled = false;
            if (Image == selectedImage)
            {
                selectedImage.enabled = false;
                selectedImage = null;
                cameraBehaviour.enabled = true;
            }
            else
            {
                if (selectedImage != default)
                {
                    selectedImage.enabled = false;
                }
                selectedImage = Image;
                selectedImage.enabled = true;
                cameraBehaviour.enabled = false;
                selectedBehaviour.enabled = true;
            }
        }
    }
}
