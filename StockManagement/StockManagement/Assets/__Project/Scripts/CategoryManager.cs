using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CategoryManager : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshUIs;
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
       // Debug.Log("Current filter level is: " + currentCategoryLevel);
       // Debug.Log("Current active column is: " + (ColumnIdentifier.ColumnNames)currentCategoryLevel);
    }

    public void SetUILabelAccordingToClickedItem(string clickedItemName, ColumnIdentifier.ColumnNames MyColumnName)
    {
        for (int i = 0; i < textMeshUIs.Length; i++)
        {
            if (i < currentCategoryLevel) continue;
            textMeshUIs[i].text = "";
        }

        foreach (UIColumnIdentifier item in FindObjectsOfType<UIColumnIdentifier>())
        {
            if (item.MyColumnMatch == MyColumnName)
            {
                item.GetComponent<TextMeshProUGUI>().text = clickedItemName + " > ";
            }
        }
    }

    /*
     AmmoType theEnum = AmmoType.End;
     int toInteger = (int)theEnum;
     AmmoType andBackAgain = (AmmoType)toInteger;
    */

}
