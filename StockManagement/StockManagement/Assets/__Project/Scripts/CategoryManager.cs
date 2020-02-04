using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour
{
    public int currentCategoryLevel;
    private List<string> filterList = new List<string>();

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

    public void AddStringToFilterList(string itemToAdd)
    {
        //go through list and remove to index sent
        Debug.Log("Current filter index is: " + currentCategoryLevel);

        filterList.Add(itemToAdd);

    }

    /*
     AmmoType theEnum = AmmoType.End;
     int toInteger = (int)theEnum;
     AmmoType andBackAgain = (AmmoType)toInteger;
    */

}
