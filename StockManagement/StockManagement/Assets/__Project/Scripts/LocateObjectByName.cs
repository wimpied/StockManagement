using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateObjectByName : MonoBehaviour
{
    [HideInInspector] public GameObject LocateObjectUsingName(string name)
    {
        return GameObject.Find(name);
    }
}
