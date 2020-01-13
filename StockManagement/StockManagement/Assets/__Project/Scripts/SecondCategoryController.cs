using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondCategoryController : MonoBehaviour
{
    [SerializeField] private GameObject thirdCategoryObjectPrefab;
    [SerializeField] private Material selectedMaterial;

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

        PopulateThirdCategoryDictionary();

        float offsetY = 0;
        foreach (var categoryItem in thirdCategory)
        {
            Debug.Log(categoryItem.Key);

            //instantiate the MainCategory prefab here
            GameObject thirdCategoryObject = Instantiate(thirdCategoryObjectPrefab);
            //offset objects here
            thirdCategoryObject.transform.position = new Vector3(this.transform.position.x + 15, this.transform.position.y - offsetY, this.transform.position.z);
            offsetY += 5;

            //pass information to instantiated categoryObject
            SecondCategoryController productCategoryDescriptionController = thirdCategoryObject.GetComponent<SecondCategoryController>();
            productCategoryDescriptionController.Init(categoryItem.Key, inventoryLineItems);

            //set names
            thirdCategoryObject.name = categoryItem.Key + "_Object";
            thirdCategoryObject.GetComponentInChildren<TextMeshPro>().text = categoryItem.Key;

            //highlight
            ShowAsSelected(thirdCategoryObject);

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
            if (string.IsNullOrEmpty(lineItem.SubArea3)) continue;

            if (!thirdCategory.TryGetValue(lineItem.SubArea3, out List<InventoryLineItem> list))
            {
                list = new List<InventoryLineItem>();
                thirdCategory.Add(lineItem.SubArea3, list);
            }

            list.Add(lineItem);
        }
    }
}

