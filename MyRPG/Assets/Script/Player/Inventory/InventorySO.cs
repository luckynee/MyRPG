using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory" , menuName = "Inventory System/Inventory")]
public class InventorySO : ScriptableObject
{
    public string savePath;

    public ItemDatabaseSO database;
    public Inventory inventoryContainer;

    public void AddItem(Item _item, int _amountItem)
    {
        if (_item.buff.Length > 0)
        {
            SetEmptySlot(_item, _amountItem);
            return;
        }

        for (int i = 0; i < inventoryContainer.itemList.Length; i++)  //Check item in inventory
        {
            if (inventoryContainer.itemList[i].ID == _item.Id)
            {
                inventoryContainer.itemList[i].AddAmount(_amountItem);
                return;
            }
        }
        SetEmptySlot(_item, _amountItem);

    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < inventoryContainer.itemList.Length; i++)
        {
            if (inventoryContainer.itemList[i].ID <= -1)
            {
                inventoryContainer.itemList[i].UpdateSlot(_item.Id, _item, _amount);
                return inventoryContainer.itemList[i];
            }
        }
        //set up function for full inventory
        return null; 
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amountItem);
        item2.UpdateSlot(item1.ID, item1.item, item1.amountItem);
        item1.UpdateSlot(temp.ID, temp.item, temp.amountItem);
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < inventoryContainer.itemList.Length; i++)
        {
            if (inventoryContainer.itemList[i].item == _item)
            {
                inventoryContainer.itemList[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        //Convert to JSON and saving the file
        /*
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
        */

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, inventoryContainer);
        stream.Close();

    }

    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)) )
        {
            //If file exist convert the file back to scriptable Object
            /*
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(),this);
            file.Close() ;
            */

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer= (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < inventoryContainer.itemList.Length; i++)
            {
                inventoryContainer.itemList[i].UpdateSlot(newContainer.itemList[i].ID, newContainer.itemList[i].item, newContainer.itemList[i].amountItem);
            }
            stream.Close();


        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        inventoryContainer = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] itemList = new InventorySlot[24];
}

[System.Serializable]
public class InventorySlot 
{
    public int ID = -1;
    public Item item;
    public int amountItem;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amountItem = 0;
    } 
    public InventorySlot(int _id,Item _item, int _amountItem)
    {
        ID = _id;
        item = _item;
        amountItem = _amountItem;
    }

    public void UpdateSlot(int _id, Item _item, int _amountItem)
    {
        ID = _id;
        item = _item;
        amountItem = _amountItem;
    }

    public void AddAmount(int value)
    {
        amountItem += value;
    }
}
