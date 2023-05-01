using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DebugStuffBehaviour : MonoBehaviour
{

    void Awake()
    {


#if !UNITY_EDITOR //Check if running a build or in editor
gameObject.SetActive(false);
        

#endif


    }
}
