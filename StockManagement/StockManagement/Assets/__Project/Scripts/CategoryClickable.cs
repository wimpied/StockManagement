using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryClickable : MonoBehaviour
{
    RetrieveData InitialData;
    LerpToDestination lerp;
    private void Start()
    {
        InitialData = FindObjectOfType<RetrieveData>();
        lerp = FindObjectOfType<LerpToDestination>();
    }

    private void OnMouseDown()
    {
        Queue<string> itemToSelect = new Queue<string>(Category);
        InitialData.AutoSelectItems(itemToSelect);
        if(GetComponentInChildren<Camera>() != null)
        lerp.SetLerpDestination(GetComponentInChildren<Camera>().transform);   
     
    }

    public string[] Category;
    

}
