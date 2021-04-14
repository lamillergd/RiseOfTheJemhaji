using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public string mapName;
    public HideNodes nodes;
    public GameObject mainCanvas;
    public GameObject playerImage;
    public GameObject mainUI;
    public GameObject uiScreens;
    public GameObject currentLevel;
    public GameObject nextLevel;
    public GameObject questsScreen;
    public GameObject inventoryScreen;
    public GameObject charScreen;
    public GameObject skillTree;
    public GameObject settingsScreen;
    public GameObject worldMap;
    public DynamicInterface inventory;
    public StaticInterface equipment;

    void Start()
    {
        playerImage.GetComponent<Image>().sprite = Manager.instance.headshot;
        SetScreens();
        SetXPBar();

        inventory.InitUI();
        equipment.InitUI();
    }

    void SetScreens()
    {
        mainUI.SetActive(true);
        uiScreens.SetActive(false);
        questsScreen.SetActive(false);
        inventoryScreen.SetActive(false);
        charScreen.SetActive(false);
        skillTree.SetActive(false);
        settingsScreen.SetActive(false);
        worldMap.SetActive(false);
    }

    void SetXPBar()
    {
        Text currentLvl = currentLevel.GetComponentInChildren<Text>();
        Text nextLvl = nextLevel.GetComponentInChildren<Text>();
        currentLvl.text = Manager.instance.level.ToString();
        nextLvl.text = (Manager.instance.level + 1).ToString();
    }

    public void UIScreens(string screen)
    {
        if (screen == "Quests")
        {
            inventoryScreen.SetActive(false);
            charScreen.SetActive(false);
            questsScreen.SetActive(true);
        }
        if (screen == "Inventory")
        {
            inventoryScreen.SetActive(true);
            charScreen.SetActive(false);
            questsScreen.SetActive(false);
        }
        if (screen == "Character")
        {
            inventoryScreen.SetActive(false);
            charScreen.SetActive(true);
            questsScreen.SetActive(false);
        }
    }

    public void ShowQuests()
    {
        uiScreens.SetActive(true);
        questsScreen.SetActive(true);
        nodes.DisableNodes();
    }
    

    public void ShowInventory()
    {
        uiScreens.SetActive(true);
        inventoryScreen.SetActive(true);
        nodes.DisableNodes();
    }

    public void ShowCharacter()
    {
        uiScreens.SetActive(true);
        charScreen.SetActive(true);
        nodes.DisableNodes();
    }

    public void ShowSettings()
    {
        uiScreens.SetActive(true);
        settingsScreen.SetActive(true);
        nodes.DisableNodes();
    }

    public void ShowMap()
    {
        uiScreens.SetActive(true);
        worldMap.SetActive(true);
        nodes.DisableNodes();
    }

    public void SaveSettings()
    {
        //save settings on screen close (PlayerPrefs)
        Debug.Log("Saved Settings");
    }

    public void CloseUIScreens()
    {
        questsScreen.SetActive(false);
        charScreen.SetActive(false);
        inventoryScreen.SetActive(false);
        skillTree.SetActive(false);
        settingsScreen.SetActive(false);
        worldMap.SetActive(false);
        uiScreens.SetActive(false);
        nodes.EnableNodes();
    }
}

//might not need Tychis
public enum Locations
{
    Tutorial,
    Tychis,
    Iulara,
    Sestia,
    Beterran,
    Strichi,
}
