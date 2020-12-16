using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Item", menuName = "Inventory/Items/Food")]
public class FoodItemSO : ItemSO
{
    public int restoreHealthValue;

    public void Awake()
    {
        type = ItemType.Food;
    }
}
