using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseSO database;
    public List<InventorySlot> container = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseSO)AssetDatabase.LoadAssetAtPath("Assets/Resources/_Item Database.asset", typeof(ItemDatabaseSO));
#else
        database = Resources.Load<ItemDatabaseSO>("_Item Database");
#endif
    }

    public void AddItem(ItemSO _item, int _amount)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item)
            {
                container[i].AddAmount(_amount);
                return;
            }
        }
        container.Add(new InventorySlot(database.getId[_item], _item, _amount));
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < container.Count; i++)
        {
            container[i].item = database.getItem[container[i].id];
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public ItemSO item;
    public int amount;

    public InventorySlot(int _id, ItemSO _item, int _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
