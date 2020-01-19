using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCategoryController : MonoBehaviour
{
    [SerializeField] private GameObject secondaryCategoryPrefab;
    [SerializeField] private Material selectedMaterial;

    public bool selected = false;
    private List<InventoryLineItem> inventoryLineItems;
    Dictionary<string, List<InventoryLineItem>> secondCategory;


    public void Init(string name, List<InventoryLineItem> lineItemList)
    {
        this.inventoryLineItems = lineItemList;
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse over this object");
    }

    private void OnMouseDown()
    {
        ExpandData();
        selected = true;
        //DeselectSiblings();
    }

    private void ExpandData()
    {
        Debug.Log("Mouse Down");
        PopulateSecondCategoryDictionary();

        float offsetY = 0;
        foreach (var categoryItem in secondCategory)
        {
            Debug.Log(categoryItem.Key);

            //instantiate the MainCategory prefab here
            GameObject categoryObject = Instantiate(secondaryCategoryPrefab, GetCorrectColumnTransform());
            //offset objects here
            categoryObject.transform.localPosition = new Vector3(0, -offsetY, 0);
            offsetY += 5;

            //pass information to instantiated categoryObject
            SecondCategoryController categoryController = categoryObject.GetComponent<SecondCategoryController>();
            categoryController.Init(categoryItem.Key, inventoryLineItems);

            //set names
            categoryObject.name = categoryItem.Key + "_Object";
            categoryObject.GetComponentInChildren<TextMeshPro>().text = categoryItem.Key;

            //highlight
            ShowAsSelected(categoryObject);

        }
    }

    //to place objects under
    public ColumnIdentifier.ColumnNames MyColumnName;
    ColumnIdentifier columnIdentifier;
    Transform columnTransform;
    Transform GetCorrectColumnTransform()
    {
        foreach (ColumnIdentifier item in FindObjectsOfType<ColumnIdentifier>())
        {
            if (item.ThisColumnName == MyColumnName)
            {
                return item.transform;
            }
        }
        return null;
    }

    private void DeselectSiblings()
    {
        foreach (var item in FindObjectsOfType<MainCategoryController>())
        {
            if (item.GetComponent<MainCategoryController>().selected)
                continue;
            else
            {
                //item.gameObject.SetActive(false);
            }

        }
    }


    private void ShowAsSelected(GameObject selectedObject)
    {
        selectedObject.GetComponentInChildren<Renderer>().material = selectedMaterial;
        if (this.GetComponentInChildren<Renderer>() != null)
            this.GetComponentInChildren<Renderer>().material = selectedMaterial;
    }

    private void PopulateSecondCategoryDictionary()
    {
        secondCategory = new Dictionary<string, List<InventoryLineItem>>();

        foreach (var lineItem in inventoryLineItems)
        {
            if (string.IsNullOrEmpty(lineItem.SubArea3)) continue;

            if (!secondCategory.TryGetValue(lineItem.SubArea3, out List<InventoryLineItem> list))
            {
                list = new List<InventoryLineItem>();
                secondCategory.Add(lineItem.SubArea3, list);
            }

            list.Add(lineItem);
        }
    }
}

