using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHighlight : MonoBehaviour
{
    public Material HighlightMat;
    public Material originalMat;
    List<Renderer> renderers = new List<Renderer>();
    [Tooltip("If true, will ignore children for highlighting")]
    public bool checkRenderersOnThisObject = false;
    private void OnMouseOver()
    {
        //if(checkRenderersOnThisObject)
        //{
        //    GetComponent<Renderer>().material = HighlightMat;
        //    return;
        //}
        foreach (Renderer item in GetComponentsInChildren<Renderer>())
        {
            item.material = HighlightMat;
            renderers.Add(item);
        }
    }

    private void OnMouseExit()
    {
        //if(checkRenderersOnThisObject)
        //{
        //    GetComponent<Renderer>().material = originalMat;
        //    return;
        //}
        foreach (Renderer item in renderers)
        {
            item.material = originalMat;
        }
    }
}
