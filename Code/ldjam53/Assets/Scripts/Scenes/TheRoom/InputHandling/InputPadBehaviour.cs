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

        //public CameraBehaviour cameraBehaviour;
        public MoveSelectedBehaviour moveSelectedBehaviour;
        public RotateSelectedBehaviour turnSelectedBehaviour;
        public MoveCamBehaviour moveCamBehaviour;
        public RotateCamBehaviour turnCamBehaviour;


        private ManipulationInterface selectedBehaviourInterface;
        private Image selectedImage;

        private ManipulationInterface defaultManipulationInterface;
        private Image defaultSelectedImage;


        public void Awake()
        {
            defaultManipulationInterface = moveSelectedBehaviour;
            defaultSelectedImage = MoveSelectedImage;
            DeselectIfAlreadySelected(defaultSelectedImage, defaultManipulationInterface);
        }

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


        private void DeselectIfAlreadySelected(Image Image, ManipulationInterface selectedBehaviour)
        {
            moveSelectedBehaviour.enabled = false;
            turnSelectedBehaviour.enabled = false;
            moveCamBehaviour.enabled = false;
            turnCamBehaviour.enabled = false;
            if (Image == selectedImage)
            {
                selectedImage.enabled = false;
                selectedImage = defaultSelectedImage;
                selectedBehaviourInterface = defaultManipulationInterface;
                selectedBehaviourInterface.enabled = true;

            }
            else
            {
                if (selectedImage != default)
                {
                    selectedImage.enabled = false;
                }
                selectedImage = Image;
                selectedImage.enabled = true;
                selectedBehaviour.enabled = true;
                selectedBehaviourInterface = selectedBehaviour;
            }
        }


        public void OnButtonBottomMiddle()
        {
            selectedBehaviourInterface.OnButtonBottomMiddle();
        }

        public void OnButtonMidleLeft()
        {
            selectedBehaviourInterface.OnButtonMidleLeft();
        }


        //public void OnButtonMiddleLeftClick()
        //{

        //    inputHandler.RotateYN();
        //}

        //public void OnButtonMiddleLeftDown()
        //{
        //    inputHandler.RotateYN();
        //}

        public void OnButtonMiddleRight()
        {
            selectedBehaviourInterface.OnButtonMiddleRight();
        }

        public void OnButtonTopLeft()
        {
            selectedBehaviourInterface.OnButtonTopLeft();
        }

        public void OnButtonTopMiddle()
        {
            selectedBehaviourInterface.OnButtonTopMiddle();
        }

        public void OnButtonTopRight()
        {
            selectedBehaviourInterface.OnButtonTopRight();
        }
    }
}
