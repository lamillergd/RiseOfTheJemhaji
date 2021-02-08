using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory/Items/Database")]
public class ItemDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemSO[] itemObjs;

    [ContextMenu("Update IDs")]
    public void UpdateID()
    {
        for (int i = 0; i < itemObjs.Length; i++)
        {
            if(itemObjs[i].data.id != i)
            {
                itemObjs[i].data.id = i;
            }
        }
    }

    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {

    }
}
