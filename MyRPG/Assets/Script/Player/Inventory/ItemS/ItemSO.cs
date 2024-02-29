using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipment,
    Default
}

public enum Attributes
{
    Agility,
    Intelegent,
    Strength
}

public abstract class ItemSO : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public ItemBuff[] buff;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buff;

    public Item(ItemSO item)
    {
        Name = item.name;
        Id = item.Id;
        buff = new ItemBuff[item.buff.Length];

        for(int i = 0; i < buff.Length; i++)
        {
            buff[i] = new ItemBuff(item.buff[i].min, item.buff[i].max)
            {
                attribute = item.buff[i].attribute
            };

        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;

    public ItemBuff(int _min, int _max)
    {
        min = _min; 
        max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}
