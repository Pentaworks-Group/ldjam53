using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Base;

using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Core.Game.PlayButtonSound();
    }
}
