using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] items;
    public Dictionary<ItemSO, int> getId = new Dictionary<ItemSO, int>();
    public Dictionary<int, ItemSO> getItem = new Dictionary<int, ItemSO>();

    public void OnAfterDeserialize()
    {
        getId = new Dictionary<ItemSO, int>();
        getItem = new Dictionary<int, ItemSO>();
        for (int i = 0; i < items.Length; i++)
        {
            getId.Add(items[i], i);
            getItem.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
