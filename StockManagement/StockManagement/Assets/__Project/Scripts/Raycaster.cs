using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    void Update()
    {
        CheckRaycastClick();
    }

    Transform hitObject;
    RaycastHighlight highlight;
    void CheckRaycastClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            hitObject = hit.transform;
            highlight = hitObject.GetComponent<RaycastHighlight>();

            if (highlight != null)
            {
                highlight.Highlight(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                //highlight.Click();
            }

            if (Input.GetMouseButtonDown(1))
            {
                //highlight.Click();
            }
        }
        else
        {
            if (highlight != null)
            {
                highlight.Highlight(false);
            }
            hitObject = null;
        }

    }
}


