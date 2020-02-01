using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour
{
    [HideInInspector] public int currentCategoryLevel;

    public void IncreaseCurrentFilterLevel()
    {
        currentCategoryLevel++;
    }

    public void DecreaseCurrentFilterLevel()
    {
        currentCategoryLevel--;
    }

    public void SetCurrentFilterLevel(int currentFilterFromClick)
    {
        currentCategoryLevel = currentFilterFromClick;
        Debug.Log("Current filter level is: " + currentCategoryLevel);
        Debug.Log("Current active column is: " + (ColumnIdentifier.ColumnNames)currentCategoryLevel);
    }

    /*
     AmmoType theEnum = AmmoType.End;
     int toInteger = (int)theEnum;
     AmmoType andBackAgain = (AmmoType)toInteger;
    */

}
