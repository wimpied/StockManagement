using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHideCategoryObjects : MonoBehaviour
{
    public bool HideOnStart = false;

    List<Renderer> meshObjects = new List<Renderer>();
    List<Renderer> textObjects = new List<Renderer>();

    void Start()
    {
        if (HideOnStart)
            ShowAll(false);
        foreach (Transform item in transform.transform)
        {
            if(item.GetComponent<MeshFilter>() != null)
            {
                meshObjects.Add(item.GetComponent<Renderer>());
            }
            
            if(item.GetComponent<TextMeshPro>() != null)
            {
                textObjects.Add(item.GetComponent<Renderer>());
            }
          
        }
    }

    public void ShowAll(bool state)
    {
        foreach (MeshFilter item in GetComponentsInChildren<MeshFilter>())
        {
            item.GetComponent<Renderer>().enabled = state;
        }

    }

    public void IsolateThisObject()
    {
        foreach (ShowHideCategoryObjects item in FindObjectsOfType<ShowHideCategoryObjects>())
        {
            if (item != this)
            {
                Debug.Log(item.gameObject.name);
                item.ShowAll(false);
            }
            else item.ShowAll(true);
        }
    }
}
