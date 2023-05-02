using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTogglerBehavior : MonoBehaviour
{
    public GameObject Tutorial;
    public void ToggleTutorial()
    {
        Tutorial.SetActive(!Tutorial.activeSelf); 
    }
}
