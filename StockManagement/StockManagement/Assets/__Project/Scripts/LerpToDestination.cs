using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToDestination : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        lerpStart = transform;
        lerpEnd = dest;
        t = 0;

    }


}
