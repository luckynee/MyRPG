using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory" , menuName = "Inventory System/Inventory")]
public class InventorySO : ScriptableObject, ISerializationCallbackReceiver
{
    private ItemDatabaseSO database;

    public string savePath;

    public List<InventorySlot> inventoryContainer = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR

        database = (ItemDatabaseSO)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/Database.asset", typeof(ItemDatabaseSO));
#else
        database = Resources.Load<ItemDatabaseSO>("Database");  
#endif
    }

    public void AddItem(ItemSO _item, int _amountItem)
    {
    
        for (int i = 0; i < inventoryContainer.Count; i++)  //Check item in inventory
        {
            if (inventoryContainer[i].item == _item) 
            {
                inventoryContainer[i].AddAmount(_amountItem);
                return;
            }
        }
        inventoryContainer.Add(new InventorySlot(database.GetId[_item],_item,_amountItem));
    }

    public void Save()
    {
        //Convert to JSON and saving the file
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)) )
        {
            //If file exist convert the file back to scriptable Object
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(),this);
            file.Close() ;
        }
    }

    public void OnBeforeSerialize()
    {
       
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0;i < inventoryContainer.Count; i++)
            inventoryContainer[i].item = database.GetItem[inventoryContainer[i].ID];
    }
}

[System.Serializable]
public class InventorySlot 
{
    public int ID;
    public ItemSO item;
    public int amountItem;
    public InventorySlot(int _id,ItemSO _item, int _amountItem)
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
