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

        public void OnMoveSelectedImagePressed()
        {
            DeselectIfAlreadySelected(MoveSelectedImage);
            moveSelectedBehaviour.enabled = true;
        }

        public void OnTurnSelectedImagePressed()
        {
            DeselectIfAlreadySelected(TurnSelectedImage);
            turnSelectedBehaviour.enabled = true;
        }

        public void OnMoveCamImagePressed()
        {
            DeselectIfAlreadySelected(MoveCamImage);
            moveCamBehaviour.enabled = true;
        }

        public void OnTurnCamImagePressed()
        {
            DeselectIfAlreadySelected(TurnCamImage);
            turnCamBehaviour.enabled = true;
        }


        private void DeselectIfAlreadySelected(Image Image)
        {
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
            }
            moveSelectedBehaviour.enabled = false;
            turnSelectedBehaviour.enabled = false;
            moveCamBehaviour.enabled = false;
            turnCamBehaviour.enabled = false;
        }
    }
}
