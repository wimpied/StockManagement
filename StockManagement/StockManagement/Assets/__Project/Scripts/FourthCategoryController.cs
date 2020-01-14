using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FourthCategoryController : MonoBehaviour
{
    [SerializeField] private GameObject fifthCategoryObjectPrefab;
    [SerializeField] private Material selectedMaterial;

    private List<InventoryLineItem> inventoryLineItems;
    Dictionary<string, List<InventoryLineItem>> fifthCategory;


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
    }

    private void ExpandData()
    {
        Debug.Log("Mouse down");
        PopulateFifthCategoryDictionary();

        float offsetY = 0;
        foreach (var categoryItem in fifthCategory)
        {
            Debug.Log(categoryItem.Key);

            //instantiate the MainCategory prefab here
            GameObject fifthCategoryObject = Instantiate(fifthCategoryObjectPrefab);
            //offset objects here
            fifthCategoryObject.transform.position = new Vector3(this.transform.position.x + 15, this.transform.position.y - offsetY, this.transform.position.z);
            offsetY += 5;

            //pass information to instantiated categoryObject
            FourthCategoryController categoryController = fifthCategoryObject.GetComponent<FourthCategoryController>();
            categoryController.Init(categoryItem.Key, inventoryLineItems);

            //set names
            fifthCategoryObject.name = categoryItem.Key + "_Object";
            fifthCategoryObject.GetComponentInChildren<TextMeshPro>().text = categoryItem.Key;
        }
    }

    private void PopulateFifthCategoryDictionary()
    {
        fifthCategory = new Dictionary<string, List<InventoryLineItem>>();

        foreach (var lineItem in inventoryLineItems)
        {
            if (string.IsNullOrEmpty(lineItem.PlantArea4)) continue;

            if (!fifthCategory.TryGetValue(lineItem.PlantArea4, out List<InventoryLineItem> list))
            {
                list = new List<InventoryLineItem>();
                fifthCategory.Add(lineItem.PlantArea4, list);
            }

            list.Add(lineItem);
        }
    }
}

