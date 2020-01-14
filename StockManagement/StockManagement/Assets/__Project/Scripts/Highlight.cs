using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private Material highlightMat;
    [SerializeField] private float scaleUpValue;
    private Material originalMat;

    private void OnMouseOver()
    {
        if(originalMat == null)
        {
            originalMat = transform.GetChild(0).GetComponent<Renderer>().material;
        }

        ChangeColor(highlightMat);
        ScaleUp(new Vector3(scaleUpValue, scaleUpValue, 1));
    }

    private void OnMouseExit()
    {
        ChangeColor(originalMat);
        ScaleUp(Vector3.one);
    }

    void ChangeColor(Material mat)
    {
        transform.GetChild(0).GetComponent<Renderer>().material = mat;
    }

    void ScaleUp(Vector3 scaleVector)
    {
        transform.GetChild(0).localScale = scaleVector;
    }
}
