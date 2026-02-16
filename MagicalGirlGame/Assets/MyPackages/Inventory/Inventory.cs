using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public UnityEvent<PickableItem> OnNewItemPicked;
    public UnityEvent<PickableItem> OnItemRemoved;
    public List<ItemInInventory> ItemsInInventory => _itemsInInventory;
    private List<PickableItem> _items= new List<PickableItem>();
    private List<ItemInInventory> _itemsInInventory = new List<ItemInInventory>();

    public void AddItemToInventory(PickableItem item)
    {
        item.gameObject.SetActive(false);
        _items.Add(item);
        if(item.IsStackable)
        {
            ItemInInventory inventoryItem = _itemsInInventory.Find(x => x.itemSO == item.ItemSO && x.associatedItems.Count < item.MaxNumberInStack);
            if (inventoryItem != null)
            {

                inventoryItem.AddToStack(item);
                
            }
            else
            {
                inventoryItem = new ItemInInventory(item);
                _itemsInInventory.Add(inventoryItem);
            }
        }
        else
        {
            ItemInInventory newItem = new ItemInInventory(item);
            _itemsInInventory.Add(newItem);
        }
    }
    public void AddItemToInventory(PickableItem item, ItemInInventory itemInInventory)
    {
        item.gameObject.SetActive(false);
        _items.Add(item);
        if (item.IsStackable && itemInInventory.associatedItems.Count<item.MaxNumberInStack)
        {
            itemInInventory.AddToStack(item);
        }
        else
        {
            itemInInventory = new ItemInInventory(item);
            _itemsInInventory.Add(itemInInventory);
        }
    }
    public void RemoveItemFromInventory(PickableItem item)
    {
        if (!_items.Contains(item)) return;
        ItemInInventory itemInInventory = _itemsInInventory.Find(x=>x.associatedItems.Contains(item));
        if (itemInInventory == null) return;
        _items.Remove(item);
        if (itemInInventory != null)
        {
            if(itemInInventory.associatedItems.Count==0)
            {
                _itemsInInventory.Remove(itemInInventory);
            }
        }
    }

}
