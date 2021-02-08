using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    public List<ItemSO> addToInv = new List<ItemSO>();

    void Start()
    {
        if (Manager.instance.lootToAdd != null)
        {
            addToInv.AddRange(Manager.instance.lootToAdd);
        }
    }

    void Update()
    {
        if (addToInv != null)
        {
            for (int i = 0; i < addToInv.Count; i++)
            {
                Manager.instance.inventory.AddItem(new Item(addToInv[i]), 1);
                addToInv.Remove(addToInv[i]);
                Manager.instance.lootToAdd = new List<ItemSO>();
            }
        }
    }
}
