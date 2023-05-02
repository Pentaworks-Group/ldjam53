using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Scenes.TheRoom;

using UnityEngine;

public class DebugStuffBehaviour : MonoBehaviour
{
#if UNITY_EDITOR //Check if running a build or in editor
    public TheRoomBehaviour theRoomBehaviour;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            theRoomBehaviour.SpawnTest();
        }
    }

#else
    void Awake()
    {
        gameObject.SetActive(false);
    }

#endif
}
