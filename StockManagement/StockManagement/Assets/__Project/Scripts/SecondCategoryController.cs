using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondCategoryController : MonoBehaviour
{
    [SerializeField] private GameObject thirdCategoryObjectPrefab;
    [SerializeField] private Material selectedMaterial;

    public bool selected = false;
    private List<InventoryLineItem> inventoryLineItems;
    Dictionary<string, List<InventoryLineItem>> thirdCategory;


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
        PopulateThirdCategoryDictionary();

        float offsetY = 0;
        foreach (var categoryItem in thirdCategory)
        {
            Debug.Log(categoryItem.Key);

            //instantiate the MainCategory prefab here
            GameObject categoryObject = Instantiate(thirdCategoryObjectPrefab, GetCorrectColumnTransform());
            //offset objects here
            categoryObject.transform.localPosition = new Vector3(0, -offsetY, 0);
            offsetY += 5;

            //pass information to instantiated categoryObject
            ThirdCategoryController categoryController = categoryObject.GetComponent<ThirdCategoryController>();
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
        foreach (var item in FindObjectsOfType<SecondCategoryController>())
        {
            if (item.GetComponent<SecondCategoryController>().selected)
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

    private void PopulateThirdCategoryDictionary()
    {
        thirdCategory = new Dictionary<string, List<InventoryLineItem>>();

        foreach (var lineItem in inventoryLineItems)
        {
            if (string.IsNullOrEmpty(lineItem.ProductCategoryDescription)) continue;

            if (!thirdCategory.TryGetValue(lineItem.ProductCategoryDescription, out List<InventoryLineItem> list))
            {
                list = new List<InventoryLineItem>();
                thirdCategory.Add(lineItem.ProductCategoryDescription, list);
            }

            list.Add(lineItem);
        }
    }
}

