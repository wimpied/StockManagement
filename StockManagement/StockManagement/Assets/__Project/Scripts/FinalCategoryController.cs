using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCategoryController : MonoBehaviour
{
    private List<InventoryLineItem> inventoryLineItems;

    public void Init(List<InventoryLineItem> lineItemList)
    {
        this.inventoryLineItems = lineItemList;
        foreach (InventoryLineItem item in lineItemList)
        {
            Debug.Log(item.BinLocation);
            Debug.Log(item.BinLocationType);
            Debug.Log(item.ReorderQTY);
            Debug.Log(item.QuantityOnOrder);
            Debug.Log(item.ItemStatus);
            Debug.Log(item.MovingAveragePrice);
            Debug.Log(item.TotalValueofItem);

        }
    }

}
