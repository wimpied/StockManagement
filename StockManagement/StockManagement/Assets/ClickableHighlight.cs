using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHighlight : MonoBehaviour
{
    public Material HighlightMat;
    public Material originalMat;
    List<Renderer> renderers = new List<Renderer>();
    private void OnMouseOver()
    {
        foreach (Renderer item in GetComponentsInChildren<Renderer>())
        {
            item.material = HighlightMat;
            renderers.Add(item);
        }
    }

    private void OnMouseExit()
    {

        foreach (Renderer item in renderers)
        {
            item.material = originalMat;
        }
    }
}
