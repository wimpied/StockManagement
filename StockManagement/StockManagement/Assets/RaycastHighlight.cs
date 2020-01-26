using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class RaycastHighlight : MonoBehaviour
{
    [SerializeField] bool scaleOnHover = false;
    [SerializeField] float scaleUpValue = 1;
    [Space(10)]
    [SerializeField] Color outlineColor;
    [SerializeField] float outlineWidth;
    public void Click()
    {
        
    }

    public bool highlighted = false;
    public void Highlight(bool state)
    {
        highlighted = state;
        Outline outline = GetComponent<Outline>();
        if(outline != null)
        outline.enabled = state;
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;

        if (!scaleOnHover) return;
        ScaleOnHover(new Vector3(scaleUpValue, 1, 1));
    }

    void ScaleOnHover(Vector3 scaleVector)
    {
        transform.GetChild(0).localScale = scaleVector;
    }
}
