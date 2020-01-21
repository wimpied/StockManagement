using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private Material highlightMat;
    [SerializeField] private float scaleUpValue;
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
        ScaleUp(new Vector3(scaleUpValue, 1, 1));
    }

    private void OnMouseExit()
    {
        if (!selected)
        {
            ChangeModelColor(originalMat);
            ChangeTextColor(Color.gray);
            ShowLight(false);
        }
        ScaleUp(Vector3.one);
    }

    private void OnMouseDown()
    {
        foreach (Highlight item in transform.parent.GetComponentsInChildren<Highlight>())
        {
            if (item != this)
            {
                item.Select(false);
                //Debug.Log(item.name);
            }
        }
        Select(true);
    }

    void ChangeModelColor(Material mat)
    {
        transform.GetChild(0).GetComponent<Renderer>().material = mat;
    }
    void ChangeTextColor(Color newCol)
    {
        transform.GetChild(1).GetComponent<TextMeshPro>().color = newCol;
    }

    void ShowLight(bool state)
    {
            if(GetComponent<Light>() != null) GetComponent<Light>().enabled = state;
    }

    void ScaleUp(Vector3 scaleVector)
    {
        transform.GetChild(0).localScale = scaleVector;
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
            }
        }
    }
}
