using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryClickable : MonoBehaviour
{
    public GenericCategoryController CategoryController;
    RetrieveData InitialData;
    LerpToDestination lerp;
    private void Start()
    {
        InitialData = FindObjectOfType<RetrieveData>();
        lerp = FindObjectOfType<LerpToDestination>();
    }

    private void OnMouseDown()
    {
        //Debug.Log("Clicked on: " + GetComponent<MeshCollider>().sharedMesh.name);

        Queue<string> itemToSelect = new Queue<string>(Category);
        InitialData.AutoSelectItems(itemToSelect);
        lerp.SetLerpDestination(GetComponentInChildren<Camera>().transform);   
     
    }

    public string[] Category;
    

}
