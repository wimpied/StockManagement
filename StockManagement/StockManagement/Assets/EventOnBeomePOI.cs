using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnBeomePOI : MonoBehaviour
{
    public UnityEvent OnBecomePOI;

    private void OnEnable()
    {
        GenericCategoryController.BecomePOI += GenericCategoryController_BecomePOI;
    }

    private void GenericCategoryController_BecomePOI(Transform obj)
    {
        OnBecomePOI.Invoke();
    }

    private void OnDisable()
    {
        GenericCategoryController.BecomePOI -= GenericCategoryController_BecomePOI;

    }
}
