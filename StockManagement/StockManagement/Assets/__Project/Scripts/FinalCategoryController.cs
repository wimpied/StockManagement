using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalCategoryController : MonoBehaviour
{
    public int binLocationIndex, qtyOnOrderIndex, statusTextIndex, totalValueIndex, stockOnHandIndex;
    private TextMeshPro binLocationText, qtyOnOrderText, statusText, totalValueText, stockOnHandText;
    private List<InventoryLineItem> inventoryLineItems;

    public void Init(List<InventoryLineItem> lineItemList)
    {
        binLocationText = transform.GetChild(binLocationIndex).GetComponent<TextMeshPro>();
        qtyOnOrderText = transform.GetChild(qtyOnOrderIndex).GetComponent<TextMeshPro>();
        statusText = transform.GetChild(statusTextIndex).GetComponent<TextMeshPro>();
        totalValueText = transform.GetChild(totalValueIndex).GetComponent<TextMeshPro>();
        stockOnHandText = transform.GetChild(stockOnHandIndex).GetComponent<TextMeshPro>();

        this.inventoryLineItems = lineItemList;
        foreach (InventoryLineItem item in lineItemList)
        {
            statusText.text = item.ItemStatus;
            binLocationText.text = item.BinLocation;
            stockOnHandText.text = item.StockOnHand.ToString();
            qtyOnOrderText.text = item.QuantityOnOrder.ToString();
            totalValueText.text = "R " + item.TotalValueofItem.ToString();
            Debug.Log(item.MovingAveragePrice);
        }
    }

}
