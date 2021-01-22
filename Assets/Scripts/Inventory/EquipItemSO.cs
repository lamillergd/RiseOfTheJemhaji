using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "Inventory/Items/Equip")]
public class EquipItemSO : ItemSO
{
    public void Awake()
    {
        type = ItemType.Chest;
    }
}
