using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GenericCategoryController : MonoBehaviour
{
    [SerializeField] private GameObject categoryPrefab;
    [SerializeField] private Material selectedMaterial;

    public bool selected = false;
    private List<InventoryLineItem> inventoryLineItems;
    Dictionary<string, List<InventoryLineItem>> category;

    public enum DataColumnName
    {
        MainCatagory2,
        SubArea3,
        ProductCategoryDescription,
        PlantArea4,
        STATGROUP5,
        STATGROUP6,
        STATGROUP7,
        STATGROUP8,
        ItemStatus
    }

    [SerializeField] private DataColumnName dataColumnName;
    public string LineItemName;
    public void Init(string name, List<InventoryLineItem> lineItemList)
    {
        this.inventoryLineItems = lineItemList;
        columnXform = GetCorrectColumnTransform();
        LineItemName = name;
    }

    private void OnMouseDown()
    {
        ExpandData();
        GetClickedFilterlevel();
        LocateItemOfInterest();
        //DeselectSiblings();
    }

    public static event Action<Transform> BecomePOI;
    ItemIdentifier[] identifiers;
    void LocateItemOfInterest()
    {

        identifiers = FindObjectsOfType<ItemIdentifier>();
        ItemIdentifier itemOfInterest = identifiers.Where(identifier => identifier.ID == LineItemName).FirstOrDefault();
        if (itemOfInterest == null) return;

        //Debug.Log(itemOfInterest.gameObject.name);
        //do things here when object is found.
        BecomePOI?.Invoke(itemOfInterest.transform);

    }


    Transform columnXform;
    List<GenericCategoryController> childCategoryControllers;
    public void ExpandData()
    {
        ExpandData(null);
    }

    public void ExpandData(Queue<string> itemToSelect)
    {
        string currentCategoryItemToSelect = "";
        if(itemToSelect != null && itemToSelect.Count > 0)
        {
            currentCategoryItemToSelect = itemToSelect.Dequeue();
        }
        selected = true;

        ClearFilter();

        int childcount = columnXform.childCount;

        PopulateCategoryDictionary();

        float offsetY = 0;

        childCategoryControllers = new List<GenericCategoryController>();

        foreach (var categoryItem in category)
        {
            //instantiate the MainCategory prefab here
            GameObject categoryObject = Instantiate(categoryPrefab, columnXform);

            //offset objects here
            categoryObject.transform.localPosition = new Vector3(0, -offsetY, 0);
            offsetY += 5;

            //pass information to instantiated categoryObject
            GenericCategoryController categoryController = categoryObject.GetComponent<GenericCategoryController>();
            if(categoryController == null)
            {
                FinalCategoryController finalCategoryController = categoryObject.GetComponent<FinalCategoryController>();
                finalCategoryController.Init(categoryItem.Value);

            }
            else
            {
                categoryController.Init(categoryItem.Key, categoryItem.Value);

                //set names
                categoryObject.name = categoryItem.Key + "_Object";
                categoryObject.GetComponentInChildren<TextMeshPro>().text = categoryItem.Key;

                if(categoryItem.Key == currentCategoryItemToSelect && !string.IsNullOrEmpty(categoryItem.Key))
                {
                    categoryController.ExpandData(itemToSelect);
                }

                childCategoryControllers.Add(categoryController);
            }

        }
    }

    /// <summary>
    /// Clear columns of old entries
    /// </summary>
    public void ClearFilter()
    {
        if (childCategoryControllers != null)
        {
            foreach (GenericCategoryController item in childCategoryControllers)
            {
                item.ClearFilter();
            }
        }

        for (int i = columnXform.childCount - 1; i >= 1; --i)
        {
            GameObject toRemove = columnXform.GetChild(i).gameObject;
            toRemove.transform.parent = null;
            GenericCategoryController categoryControllerToRemove = toRemove.GetComponent<GenericCategoryController>();
            if(categoryControllerToRemove != null)
            {
                categoryControllerToRemove.ClearFilter();
            }
            Destroy(toRemove);
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

    void GetCorrectUILabel()
    {
        foreach (UIColumnIdentifier item in FindObjectsOfType<UIColumnIdentifier>())
        {
            if (item.MyColumnMatch == MyColumnName)
            {
                item.GetComponent<TextMeshProUGUI>().text = item.name;
            }
        }
    }

    void GetClickedFilterlevel()
    {
        FindObjectOfType<CategoryManager>().SetCurrentFilterLevel((int)GetCorrectColumnTransform().GetComponent<ColumnIdentifier>().ThisColumnName);

    }


    private void PopulateCategoryDictionary()
    {
        category = new Dictionary<string, List<InventoryLineItem>>();

        foreach (var lineItem in inventoryLineItems)
        {
            string fieldValue = "";

            switch (dataColumnName)
            {
                case DataColumnName.MainCatagory2:
                    fieldValue = lineItem.MainCatagory2;
                    break;
                case DataColumnName.SubArea3:
                    fieldValue = lineItem.SubArea3;
                    break;
                case DataColumnName.ProductCategoryDescription:
                    fieldValue = lineItem.ProductCategoryDescription;
                    break;
                case DataColumnName.PlantArea4:
                    fieldValue = lineItem.PlantArea4;
                    break;
                case DataColumnName.STATGROUP5:
                    fieldValue = lineItem.STATGROUP5;
                    break;
                case DataColumnName.STATGROUP6:
                    fieldValue = lineItem.STATGROUP6;
                    break;
                case DataColumnName.STATGROUP7:
                    fieldValue = lineItem.STATGROUP7;
                    break;
                case DataColumnName.STATGROUP8:
                    fieldValue = lineItem.STATGROUP8;
                    break;
                case DataColumnName.ItemStatus:
                    fieldValue = lineItem.ItemStatus;
                    break;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(fieldValue)) continue;

            if (!category.TryGetValue(fieldValue, out List<InventoryLineItem> list))
            {
                list = new List<InventoryLineItem>();
                category.Add(fieldValue, list);
            }
            //lineItem is the row containing all the entries.
            list.Add(lineItem);
            
            //if the gameObject contains a ItemProperties component, send last 12 columns' info to ItemProperties Component
            //display properties contained within the ItemProperties Component
        }
    }

    void DestroyInstantiatedLists(Transform myTransform)
    {
        foreach (GenericCategoryController item in FindObjectsOfType<GenericCategoryController>())
        {
            if (item.transform.parent == myTransform.transform.parent) continue;
            else Destroy(item.gameObject);
        }
    }

    
}



