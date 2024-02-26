using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory" , menuName = "Inventory System/Inventory")]
public class InventorySO : ScriptableObject
{
    public List<InventorySlot> inventoryContainer = new List<InventorySlot>();

    public void AddItem(ItemSO _item, int _amountItem)
    {
        bool hasItem = false;

        for (int i = 0; i < inventoryContainer.Count; i++)  //Check item in inventory
        {
            if (inventoryContainer[i].item == _item) 
            {
                inventoryContainer[i].AddAmount(_amountItem);
                hasItem = true;
                break;
            }
        }
        if(!hasItem) // No same item in Inventory
        {
            inventoryContainer.Add(new InventorySlot(_item,_amountItem));
        }
    }
}

[System.Serializable]
public class InventorySlot 
{
    public ItemSO item;
    public int amountItem;
    public InventorySlot(ItemSO _item, int _amountItem)
    {
        item = _item;
        amountItem = _amountItem;
    }

    public void AddAmount(int value)
    {
        amountItem += value;
    }
}
