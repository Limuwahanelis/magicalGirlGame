using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInInventory 
{
    public ItemInInventory(PickableItem item)
    {
        itemSO = item.ItemSO;
        associatedItems = new List<PickableItem>
        {
            item
        };
    }
    public void AddToStack(PickableItem item)
    {
        associatedItems.Add(item);
    }
    public PickableItemSO itemSO;
    public List<PickableItem> associatedItems;

}
