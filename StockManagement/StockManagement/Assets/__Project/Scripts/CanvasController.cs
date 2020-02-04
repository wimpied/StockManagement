using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private void Start()
    {
        foreach (Collider item in FindObjectsOfType<Collider>())
        {
            //item.enabled = false;
        }
    }

    public void EnableColliders()
    {
        foreach (Collider item in FindObjectsOfType<Collider>())
        {
            Debug.Log(item.name + " enabled");
            item.enabled = true;
        }
    }
}
