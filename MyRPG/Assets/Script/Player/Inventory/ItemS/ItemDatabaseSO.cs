using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName ="Inventory System/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] Items;
    public Dictionary<ItemSO, int> GetId = new Dictionary<ItemSO, int>();
    public Dictionary<int, ItemSO> GetItem = new Dictionary<int, ItemSO>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<ItemSO, int>();
        GetItem = new Dictionary<int, ItemSO>();

        for(int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);   
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
