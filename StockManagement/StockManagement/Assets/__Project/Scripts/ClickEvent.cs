using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour
{
    public UnityEvent ClickedEvent;

    private void OnMouseDown()
    {
        ClickedEvent.Invoke();
    }
}
