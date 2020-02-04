using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToDestination : MonoBehaviour
{

    private void OnEnable()
    {
        GenericCategoryController.BecomePOI += SetLerpDestinationToCamera;
    }

    void Update()
    {
        if (t >= 1) return;
        t += Time.deltaTime / animationTime;
        transform.position = Vector3.Lerp(lerpStart.position, lerpEnd.position, t);
        transform.rotation = Quaternion.Lerp(lerpStart.rotation, lerpEnd.rotation, t);
    }

    private Transform lerpStart, lerpEnd;
    private float t = 1; //lerp percentage
    public float animationTime = 3;
    public void SetLerpDestination(Transform dest)
    {
        if(dest.transform.parent.gameObject != null)
        Debug.Log("Lerping to: " + dest.transform.parent.gameObject.name);
        lerpStart = transform;
        lerpEnd = dest;
        t = 0;

    }

    public void SetLerpDestinationToCamera(Transform dest)
    {
        if (dest.GetComponentInChildren<Camera>() != null)
            SetLerpDestination(dest.GetComponentInChildren<Camera>().transform);
        //else Debug.Log("No Camera to move to...");
    }

    private void OnDisable()
    {
        GenericCategoryController.BecomePOI -= SetLerpDestinationToCamera;

    }


}
