using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory/Items/Consumable")]
public class ConsumableItemSO : ItemSO
{
    public void Awake()
    {
        type = ItemType.Consumable;
    }
}
