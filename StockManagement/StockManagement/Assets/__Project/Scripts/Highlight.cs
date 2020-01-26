using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Highlight : MonoBehaviour
{
    [SerializeField] private Material highlightMat;
    [SerializeField] bool scaleOnHover = true;
    [SerializeField] private float scaleUpValue = 1;
    [Space(10)]
    [SerializeField] float outlineWidth;
    [SerializeField] Color outlineColor;

    private Material originalMat;

    private void Start()
    {
        ChangeTextColor(Color.gray);
    }

    private void OnMouseOver()
    {
        if (selected) Select(true);
        if(originalMat == null)
        {
            originalMat = transform.GetChild(0).GetComponent<Renderer>().material;
        }

        ChangeModelColor(highlightMat);
        ChangeTextColor(Color.yellow);
        ShowLight(true);
        ShowOutline(true);
        if(scaleOnHover)
        ScaleUp(new Vector3(scaleUpValue, 1, 1));
    }

    private void OnMouseExit()
    {
        if (!selected)
        {
            ChangeModelColor(originalMat);
            ChangeTextColor(Color.gray);
            ShowLight(false);
            ShowOutline(false);
        }
        
        if(scaleOnHover)
        ScaleUp(Vector3.one);
    }

    private void OnMouseDown()
    {
        
        foreach (Highlight item in transform.parent.GetComponentsInChildren<Highlight>())
        {
            if (item == null) continue;
            if (item != this)
            {
                item.Select(false);
            }
        }
        Select(true);
    }

    void ChangeModelColor(Material mat)
    {
        if (transform.GetChild(0).GetComponent<Renderer>() != null)
            transform.GetChild(0).GetComponent<Renderer>().material = mat;
    }
    void ChangeTextColor(Color newCol)
    {
        if(GetComponentInChildren<TextMeshPro>() != null)
            GetComponentInChildren<TextMeshPro>().color = newCol;
    }
    void ShowLight(bool state)
    {
            if(GetComponent<Light>() != null) GetComponent<Light>().enabled = state;
    }
    void ScaleUp(Vector3 scaleVector)
    {
        transform.GetChild(0).localScale = scaleVector;
    }
    void ShowOutline(bool state)
    {
        Outline outline = GetComponent<Outline>();

        if (outline != null)
        {
            outline.OutlineColor = outlineColor;
            outline.OutlineWidth = outlineWidth;
        }
    }

    public Material SelectedMat;
    bool selected = false;
    public void Select(bool state)
    {
        if (state)
        {
            selected = true;
            ChangeModelColor(SelectedMat);
        }
        else
        {
            selected = false;
            if (originalMat != null)
            {
                ChangeModelColor(originalMat);
                ChangeTextColor(Color.gray);
                ShowLight(false);
                ShowOutline(false);

            }
        }
    }
}
