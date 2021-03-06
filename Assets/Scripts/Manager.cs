﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    [Header("Player Info")]
    public string playerName;
    public PlayerClasses playerClass;
    public Sprite fullBody;
    public Sprite headshot;
    public bool isFemale;
    public int neededXP;
    public InventorySO inventory;
    public InventorySO equipment;
    public List<GameObject> abilities;

    [Header("Player Stats")]
    public bool tutorialComplete;
    public bool combatTutComplete;
    public int health;
    public int mana;
    public int level;
    public int currentXP;

    [Header("Map Info")]
    public int currentNodeID;
    public SafezoneSO currentSafezone;
    public int combatLevel;

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
        neededXP = 100;
    }

    public void CheckProgress(int _currentNodeID)
    {
        if (_currentNodeID < PlayerPrefs.GetInt("Node Progress"))
        {
            return;
        }
        else
        {
            int newID = _currentNodeID + 1;
            PlayerPrefs.SetInt("Node Progress", newID);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Save();
        equipment.Save();

        inventory.Clear();
        equipment.Clear();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            inventory.Save();
            equipment.Save();
            //Automatically saves during ApplicationQuit
            PlayerPrefs.Save();
        }
    }
}
