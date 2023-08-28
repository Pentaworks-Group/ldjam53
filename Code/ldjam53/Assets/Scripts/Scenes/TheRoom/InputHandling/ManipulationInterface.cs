using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Scenes.TheRoom.InputHandling
{
    public abstract class ManipulationInterface : MonoBehaviour
    {

        abstract public void OnButtonBottomMiddle();

        abstract public void OnButtonMidleLeft();

        abstract public void OnButtonMiddleRight();

        abstract public void OnButtonTopLeft();

        abstract public void OnButtonTopMiddle();

        abstract public void OnButtonTopRight();
    }
}