using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName ="Inventory System/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] Items;
    public Dictionary<int, ItemSO> GetItem = new Dictionary<int, ItemSO>();

    public void OnAfterDeserialize()
    {

        for(int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetItem.Add(i, Items[i]);   
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemSO>();

    }
}
