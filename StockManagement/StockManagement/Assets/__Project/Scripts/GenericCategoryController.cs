using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        STATGROUP8
    }

    [SerializeField] private DataColumnName dataColumnName;

    public void Init(string name, List<InventoryLineItem> lineItemList)
    {
        this.inventoryLineItems = lineItemList;
        columnXform = GetCorrectColumnTransform();

    }

    private void OnMouseDown()
    {
        ExpandData();
        selected = true;
        //DeselectSiblings();
    }

    Transform columnXform;
    List<GenericCategoryController> childCategoryControllers;
    private void ExpandData()
    {
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

                childCategoryControllers.Add(categoryController);
            }

            //highlight
            ShowAsSelected(categoryObject);
        }
    }

    /// <summary>
    /// Clear columns of old entries
    /// </summary>
    void ClearFilter()
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

    private void ShowAsSelected(GameObject selectedObject)
    {
        selectedObject.GetComponentInChildren<Renderer>().material = selectedMaterial;
        if (this.GetComponentInChildren<Renderer>() != null)
            this.GetComponentInChildren<Renderer>().material = selectedMaterial;
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



