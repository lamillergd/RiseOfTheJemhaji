using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Chest,
    Legs,
    Boots,
    Gloves,
    Weapon,
    Shield,
    Default
}

public enum Attributes
{
    Armour,
    Damage,
    Vitality,
    Intellect
}

public abstract class ItemSO : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public ItemBuff[] buffs;

    public Item()
    {
        name = "";
        id = -1;
    }

    public Item(ItemSO item)
    {
        name = item.name;
        id = item.Id;
        buffs = new ItemBuff[item.buffs.Length];

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max)
            {
                attribute = item.buffs[i].attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int min;
    public int max;
    public int value;

    public ItemBuff (int _min, int _max)
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
