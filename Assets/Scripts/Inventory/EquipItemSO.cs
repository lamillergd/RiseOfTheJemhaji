using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "Inventory/Items/Equip")]
public class EquipItemSO : ItemSO
{
    //might need to move to item/data
    public PlayerClasses requiredClass;

    public void Awake()
    {
        
    }
}
