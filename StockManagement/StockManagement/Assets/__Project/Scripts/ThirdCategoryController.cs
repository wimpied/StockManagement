using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThirdCategoryController : MonoBehaviour
{
    [SerializeField] private GameObject fourthCategoryObjectPrefab;
    [SerializeField] private Material selectedMaterial;

    public bool selected = false;
    private List<InventoryLineItem> inventoryLineItems;
    Dictionary<string, List<InventoryLineItem>> fourthCategory;


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
        Debug.Log("Mouse down");
        PopulateFourthCategoryDictionary();

        float offsetY = 0;
        foreach (var categoryItem in fourthCategory)
        {
            Debug.Log(categoryItem.Key);

            //instantiate the MainCategory prefab here
            GameObject categoryObject = Instantiate(fourthCategoryObjectPrefab, GetCorrectColumnTransform());
            //offset objects here
            categoryObject.transform.localPosition = new Vector3(0, -offsetY, 0);
            offsetY += 5;

            //pass information to instantiated categoryObject
            FourthCategoryController categoryController = categoryObject.GetComponent<FourthCategoryController>();
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
        foreach (var item in FindObjectsOfType<ThirdCategoryController>())
        {
            if (item.GetComponent<ThirdCategoryController>().selected)
                continue;
            else item.gameObject.SetActive(false);
        }
    }


    private void ShowAsSelected(GameObject selectedObject)
    {
        selectedObject.GetComponentInChildren<Renderer>().material = selectedMaterial;
        if (this.GetComponentInChildren<Renderer>() != null)
            this.GetComponentInChildren<Renderer>().material = selectedMaterial;
    }

    private void PopulateFourthCategoryDictionary()
    {
        fourthCategory = new Dictionary<string, List<InventoryLineItem>>();

        foreach (var lineItem in inventoryLineItems)
        {
            if (string.IsNullOrEmpty(lineItem.STATGROUP5)) continue;

            if (!fourthCategory.TryGetValue(lineItem.STATGROUP5, out List<InventoryLineItem> list))
            {
                list = new List<InventoryLineItem>();
                fourthCategory.Add(lineItem.STATGROUP5, list);
            }

            list.Add(lineItem);
        }
    }
}

