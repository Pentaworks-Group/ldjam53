using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseExcapeBehavior : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }
}
