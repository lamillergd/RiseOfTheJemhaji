using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Database", menuName = "Enemy/Database")]
public class EnemyDatabaseSO : ScriptableObject, ISerializationCallbackReceiver
{
    public EnemySO[] allEnemies;

    [ContextMenu("Update IDs")]
    public void UpdateID()
    {
        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (allEnemies[i].ID != i)
            {
                allEnemies[i].ID = i;
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
