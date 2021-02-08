using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    [Header("Player Info")]
    public string playerName;
    public PlayerClasses playerClass;
    public string appearance;
    public int level;
    public InventorySO inventory;
    public InventorySO equipment;

    [Header("Player Stats")]
    public int health;
    public int mana;

    [Header("Map Info")]
    public GameObject nodeObject;
    public MapNode currentNode;

    public List<ItemSO> lootToAdd = new List<ItemSO>();

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        health = 100;
        mana = 100;
        level = 1;
    }

    void Start()
    {
  
    }

    void Update()
    {
        if (nodeObject != null)
        {
            currentNode = nodeObject.GetComponent<MapNode>();
            nodeObject.SetActive(false);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Save();
        equipment.Save();

        //for testing new game and continue game stuff (inv only atm)
        //File.Delete(string.Concat(Application.persistentDataPath, inventory.savePath));
        //File.Delete(string.Concat(Application.persistentDataPath, equipment.savePath));

        inventory.Clear();
        equipment.Clear();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            inventory.Save();
            equipment.Save();
        }
    }
}
