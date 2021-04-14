using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public InventorySO _equipment;
    public StatUIText statText;
    public Attribute[] attributes;

    void Start()
    {
        _equipment = Manager.instance.equipment;

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }

        for (int i = 0; i < _equipment.GetSlots.Length; i++)
        {
            _equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            _equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObj == null)
        {
            return;
        }

        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;

            case InterfaceType.Equipment:
                
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                    }
                }

                break;

            case InterfaceType.Chest:
                break;

            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObj == null)
        {
            return;
        }

        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;

            case InterfaceType.Equipment:
                
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }
                }

                if (_slot.ItemObj.type == ItemType.Weapon && Manager.instance.abilities.Count == 0)
                {
                    AddAbilties(_slot);
                }

                break;

            case InterfaceType.Chest:
                break;

            default:
                break;
        }
    }

    public void AddAbilties (InventorySlot _slot)
    {
        Manager.instance.abilities = new List<GameObject>();

        if (_slot.ItemObj.type == ItemType.Weapon && Manager.instance.abilities.Count == 0)
        {
            for (int i = 0; i < _slot.ItemObj.data.abilities.Count; i++)
            {
                Manager.instance.abilities.Add(_slot.ItemObj.data.abilities[i]);
                print(_slot.ItemObj.data.abilities[i].name.ToString() + " added");
            }
        }  
    }
}
