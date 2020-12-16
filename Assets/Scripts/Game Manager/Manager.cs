using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    [Header("Player Info")]
    public string playerName;
    public string playerClass;
    public string appearance;
    public InventorySO inventory;

    [Header("Player Stats")]
    public int level;
    public int health;
    public int mana;

    [Header("Map Info")]
    public GameObject nodeObject;
    public MapNode currentNode;

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
        inventory.container.Clear();
    }
}
