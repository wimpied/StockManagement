using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryClickable : MonoBehaviour
{
    private Mesh meshToAssignToCollider;
        
    void AssignMeshToCollider()
    {
        if (GetComponent<MeshCollider>() == null) return;

        GetComponent<MeshCollider>().sharedMesh = GetComponentInChildren<MeshFilter>().mesh;
        GetComponent<MeshCollider>().convex = true;
    }

    private void Start()
    {
        AssignMeshToCollider();
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on: " + GetComponent<MeshCollider>().sharedMesh.name);
    }

}
