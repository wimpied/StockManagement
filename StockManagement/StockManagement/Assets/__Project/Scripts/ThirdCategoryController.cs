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
        DeselectSiblings();
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
            GameObject fourthCategoryObject = Instantiate(fourthCategoryObjectPrefab);
            //offset objects here
            fourthCategoryObject.transform.position = new Vector3(this.transform.position.x + 15, this.transform.position.y - offsetY, this.transform.position.z);
            offsetY += 5;

            //pass information to instantiated categoryObject
            FourthCategoryController categoryController = fourthCategoryObject.GetComponent<FourthCategoryController>();
            categoryController.Init(categoryItem.Key, inventoryLineItems);

            //set names
            fourthCategoryObject.name = categoryItem.Key + "_Object";
            fourthCategoryObject.GetComponentInChildren<TextMeshPro>().text = categoryItem.Key;

            //highlight
            ShowAsSelected(fourthCategoryObject);

        }
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

