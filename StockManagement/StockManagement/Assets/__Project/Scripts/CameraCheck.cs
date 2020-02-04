using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCheck : MonoBehaviour
{

    private void Start()
    {
        foreach (Camera item in FindObjectsOfType<Camera>())
        {
            if (item.tag != "MainCamera") item.enabled = false;
        }
    }

}
