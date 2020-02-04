using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHighlight : MonoBehaviour
{
    List<GameObject> highlightObjs = new List<GameObject>();

    private void Start()
    {
        foreach (ClickableHighlightIdentifier item in GetComponentsInChildren<ClickableHighlightIdentifier>())
        {
            highlightObjs.Add(item.gameObject);
        }

        ShowObjects(false);
    }

    private void OnMouseOver()
    {
        ShowObjects(true);
        
    }

    private void OnMouseExit()
    {
        ShowObjects(false);
    }

    void ShowObjects(bool state)
    {
        foreach (GameObject item in highlightObjs)
        {
            item.SetActive(state);
        }
    }
}
