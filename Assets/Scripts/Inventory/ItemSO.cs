using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
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
    Damage,
    Mastery,
    Armour,
    Health,
    Mana,
    Cooldown_Rate
}

public abstract class ItemSO : ScriptableObject
{
    public Sprite uiDisplay;
    public bool isStackable;
    public ItemType type;
    [TextArea(15,20)]
    public string description;

    public Item data = new Item();

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
    public int id = -1;
    public ItemBuff[] buffs;
    public List<GameObject> abilities;

    public Item()
    {
        name = "";
        id = -1;
    }

    public Item(ItemSO item)
    {
        name = item.name;
        id = item.data.id;
        buffs = new ItemBuff[item.data.buffs.Length];

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff : IModifiers
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

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }
}
