using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainCategoryController : MonoBehaviour
{
    [SerializeField] private GameObject secondaryCategoryPrefab;
    [SerializeField] private Material selectedMaterial;

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
            GameObject secondaryCatgoryObject = Instantiate(secondaryCategoryPrefab);
            //offset objects here
            secondaryCatgoryObject.transform.position = new Vector3(this.transform.position.x + 15, this.transform.position.y - offsetY, this.transform.position.z);
            offsetY += 5;

            //pass information to instantiated categoryObject
            SecondCategoryController categoryController = secondaryCatgoryObject.GetComponent<SecondCategoryController>();
            categoryController.Init(categoryItem.Key, inventoryLineItems);

            //set names
            secondaryCatgoryObject.name = categoryItem.Key + "_Object";
            secondaryCatgoryObject.GetComponentInChildren<TextMeshPro>().text = categoryItem.Key;
        }
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

