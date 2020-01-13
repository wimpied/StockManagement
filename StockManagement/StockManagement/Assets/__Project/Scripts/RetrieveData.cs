using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class RetrieveData : MonoBehaviour
{
    [SerializeField] private GameObject mainCategoryPrefab;
    [SerializeField] private Material selectedMaterial;

    #region Importing from CSV for prototyping

    List<InventoryLineItem> inventoryLineItems;
    Dictionary<string, List<InventoryLineItem>> mainCategory;
    private void Start()
    {
        LoadDataFromCSV();
  
        PopulateMainCategoryDictionary();

        float offsetY = 0;
        foreach (var categoryItem in mainCategory)
        {
            Debug.Log(categoryItem.Key);

            //instantiate the MainCategory prefab here
            GameObject newMainCategoryObject = Instantiate(mainCategoryPrefab);
            //offset objects here
            newMainCategoryObject.transform.position = new Vector3(this.transform.position.x +10, this.transform.position.y - offsetY, this.transform.position.z);
            offsetY += 5;

            //pass information to instantiated categoryObject
            MainCategoryController mainCategoryController = newMainCategoryObject.GetComponent<MainCategoryController>();
            mainCategoryController.Init(categoryItem.Key, categoryItem.Value);

            //set names
            newMainCategoryObject.name = categoryItem.Key + "_Object";
            newMainCategoryObject.GetComponentInChildren<TextMeshPro>().text = categoryItem.Key;

            //highlight
            ShowAsSelected(newMainCategoryObject);
        }

        Debug.Log(inventoryLineItems.Count + " line items retrieved from CSV");
    }

    private void ShowAsSelected(GameObject selectedObject)
    {
        selectedObject.GetComponentInChildren<Renderer>().material = selectedMaterial;
        if(this.GetComponentInChildren<Renderer>() != null)
            this.GetComponentInChildren<Renderer>().material = selectedMaterial;
    }


    private void PopulateMainCategoryDictionary()
    {
        mainCategory = new Dictionary<string, List<InventoryLineItem>>();

        foreach (var lineItem in inventoryLineItems)
        {
            if (string.IsNullOrEmpty(lineItem.MainCatagory2)) continue;

            if (!mainCategory.TryGetValue(lineItem.MainCatagory2, out List<InventoryLineItem> list))
            {
                list = new List<InventoryLineItem>();
                mainCategory.Add(lineItem.MainCatagory2, list);
            }

            list.Add(lineItem);
        }
    }

    private void LoadDataFromCSV()
    {
        Debug.Log("Loading Data from CSV");
        inventoryLineItems = new List<InventoryLineItem>();

        StreamReader sReader = null;
        FileInfo statusFile = new FileInfo("Inventory.csv");
        if (statusFile != null && statusFile.Exists)
        {
            sReader = (StreamReader)statusFile.OpenText();
            sReader.ReadLine(); // Skip header row
            while (!sReader.EndOfStream)
            {
                var values = removeQuotedStrings(sReader.ReadLine()).Split(',');
                for (int i = 0; i < values.Length; i++)
                    values[i] = values[i].Replace("&Comma&", ",");

                InventoryLineItem newLineItem = new InventoryLineItem();

                newLineItem.ProductCode = values[0];
                newLineItem.ProductDescription = values[1];
                int.TryParse(values[2], out newLineItem.ProductCategoryCode);
                newLineItem.TharisaPLC1 = values[3];
                newLineItem.MainCatagory2 = values[4];
                newLineItem.SubArea3 = values[5];
                newLineItem.ProductCategoryDescription = values[6];
                newLineItem.PlantArea4 = values[7];
                newLineItem.STATGROUP5 = values[8];
                newLineItem.STATGROUP6 = values[9];
                newLineItem.STATGROUP7 = values[10];
                newLineItem.STATGROUP8 = values[11];
                newLineItem.ItemStatus = values[12];
                newLineItem.StatisticalGroupCode = values[13];
                newLineItem.StatisticalGroupCodeDescription = values[14];
                newLineItem.BinLocation = values[15];
                newLineItem.BinLocationType = values[16];
                int.TryParse(values[17], out newLineItem.QuantityOnOrder);
                int.TryParse(values[18], out newLineItem.StockOnHand);
                int.TryParse(values[19], out newLineItem.MaxStockQTY);
                int.TryParse(values[20], out newLineItem.ReorderQTY);
                float.TryParse(values[21], out newLineItem.SafetyStockQTY);
                float.TryParse(values[22], out newLineItem.MovingAveragePrice);
                float.TryParse(values[23], out newLineItem.TotalValueofItem);

                inventoryLineItems.Add(newLineItem);

            }
            sReader.Close();
        }

    }

    private string removeQuotedStrings(string input)
    {
        string output = "";
        int index = input.IndexOf("\"");
        while (index >= 0)
        {
            output += input.Substring(0, index);
            input = input.Substring(index + 1);
            index = input.IndexOf("\"");
            output += input.Substring(0, index).Replace(",", "&Comma&");
            input = input.Substring(index + 1);
            index = input.IndexOf("\"");
        }

        output += input;
        return output;
    }
    #endregion


}

public class InventoryLineItem
{
    public string ProductCode;
    public string ProductDescription;
    public int ProductCategoryCode;
    public string TharisaPLC1;
    public string MainCatagory2;
    public string SubArea3;
    public string ProductCategoryDescription;
    public string PlantArea4;
    public string STATGROUP5;
    public string STATGROUP6;
    public string STATGROUP7;
    public string STATGROUP8;
    public string ItemStatus;
    public string StatisticalGroupCode;
    public string StatisticalGroupCodeDescription;
    public string BinLocation;
    public string BinLocationType;
    public int QuantityOnOrder;
    public int StockOnHand;
    public int MaxStockQTY;
    public int ReorderQTY;
    public float SafetyStockQTY;
    public float MovingAveragePrice;
    public float TotalValueofItem;

    public override string ToString()
    {
        return ProductCode + " " +
ProductDescription + " " +
ProductCategoryCode + " " +
TharisaPLC1 + " " +
MainCatagory2 + " " +
SubArea3 + " " +
ProductCategoryDescription + " " +
PlantArea4 + " " +
STATGROUP5 + " " +
STATGROUP6 + " " +
STATGROUP7 + " " +
STATGROUP8 + " " +
ItemStatus + " " +
StatisticalGroupCode + " " +
StatisticalGroupCodeDescription + " " +
BinLocation + " " +
BinLocationType + " " +
QuantityOnOrder + " " +
StockOnHand + " " +
MaxStockQTY + " " +
ReorderQTY + " " +
SafetyStockQTY + " " +
MovingAveragePrice + " " +
TotalValueofItem + " ";

    }
}
