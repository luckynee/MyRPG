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
        if(_item.buff.Length > 0)
        {
            inventoryContainer.itemList.Add(new InventorySlot(_item.Id, _item, _amountItem));
            return;
        }

        for (int i = 0; i < inventoryContainer.itemList.Count; i++)  //Check item in inventory
        {
            if (inventoryContainer.itemList[i].item.Id == _item.Id) 
            {
                inventoryContainer.itemList[i].AddAmount(_amountItem);
                return;
            }
        }
        inventoryContainer.itemList.Add(new InventorySlot(_item.Id,_item,_amountItem));
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
            inventoryContainer = (Inventory)formatter.Deserialize(stream);
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
    public List<InventorySlot> itemList = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot 
{
    public int ID;
    public Item item;
    public int amountItem;
    public InventorySlot(int _id,Item _item, int _amountItem)
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
