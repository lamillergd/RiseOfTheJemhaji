using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject uiScreens;
    public GameObject menuPopup;
    public GameObject currentLevel;
    public GameObject nextLevel;
    public GameObject questsScreen;
    public GameObject inventoryScreen;
    public GameObject charScreen;
    public GameObject skillTree;
    public GameObject settingsScreen;
    public GameObject worldMap;

    void Start()
    {
        SetScreens();
        SetXPBar();
    }

    void SetScreens()
    {
        mainUI.SetActive(true);
        menuPopup.SetActive(false);
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

    public void PopupManager()
    {
        if (menuPopup.activeInHierarchy == false)
        {
            menuPopup.SetActive(true);
        }
        else
        {
            menuPopup.SetActive(false);
        }
    }

    public void ShowQuests()
    {
        uiScreens.SetActive(true);
        questsScreen.SetActive(true);
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

    public void ShowInventory()
    {
        uiScreens.SetActive(true);
        inventoryScreen.SetActive(true);
    }

    public void ShowCharacter()
    {
        uiScreens.SetActive(true);
        charScreen.SetActive(true);
    }

    public void ShowSettings()
    {
        uiScreens.SetActive(true);
        settingsScreen.SetActive(true);
    }

    public void SaveSettings()
    {
        //save settings on screen close
        Debug.Log("Saved Settings");
    }

    public void ShowMap()
    {
        uiScreens.SetActive(true);
        worldMap.SetActive(true);
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
        if (menuPopup.activeInHierarchy == true)
        {
            menuPopup.SetActive(false);
        }
    }
}
