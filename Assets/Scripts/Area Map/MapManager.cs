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
    public Slider xpBar;
    public GameObject currentLevel;
    public GameObject nextLevel;
    public GameObject xpText;
    public GameObject questsScreen;
    public GameObject inventoryScreen;
    public GameObject charScreen;
    public GameObject skillTree;
    public GameObject settingsScreen;
    public Slider volumeSlider;
    public GameObject worldMap;
    public GameObject tutorialText;
    public DynamicInterface inventory;
    public StaticInterface equipment;

    [Header("Level/Scene Loading")]
    public GameObject levelLoaderObj;
    LevelLoader levelLoader;

    int neededXP;
    int currentXP;

    void Start()
    {
        playerImage.GetComponent<Image>().sprite = Manager.instance.headshot;
        neededXP = Manager.instance.neededXP;
        currentXP = Manager.instance.currentXP;
        volumeSlider.value = AudioManager.instance.volume;
        SetScreens();
        SetXPBar();

        inventory.InitUI();
        equipment.InitUI();
        levelLoader = levelLoaderObj.GetComponent<LevelLoader>();

        if (tutorialText != null)
        {
            if (Manager.instance.tutorialComplete == false)
            {
                tutorialText.SetActive(true);
                nodes.DisableNodes();
            }
            else
            {
                tutorialText.SetActive(false);
            }
        }
        else { return; }
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
        xpBar.maxValue = neededXP;
        xpBar.value = currentXP;
        Text XPText = xpText.GetComponent<Text>();
        XPText.text = currentXP + "/" + neededXP;
    }

    public void CloseTutorial()
    {
        Destroy(tutorialText);
        Manager.instance.tutorialComplete = true;
        nodes.EnableNodes();
        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void UIScreens(string screen)
    {
        if (screen == "Quests")
        {
            inventoryScreen.SetActive(false);
            charScreen.SetActive(false);
            questsScreen.SetActive(true);
            AudioManager.instance.Play("GeneralButtonClick");
        }
        if (screen == "Inventory")
        {
            inventoryScreen.SetActive(true);
            charScreen.SetActive(false);
            questsScreen.SetActive(false);
            AudioManager.instance.Play("GeneralButtonClick");
        }
        if (screen == "Character")
        {
            inventoryScreen.SetActive(false);
            charScreen.SetActive(true);
            questsScreen.SetActive(false);
            AudioManager.instance.Play("GeneralButtonClick");
        }
    }

    public void ShowQuests()
    {
        uiScreens.SetActive(true);
        questsScreen.SetActive(true);
        nodes.DisableNodes();
        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void ShowInventory()
    {
        uiScreens.SetActive(true);
        inventoryScreen.SetActive(true);
        nodes.DisableNodes();
        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void ShowCharacter()
    {
        uiScreens.SetActive(true);
        charScreen.SetActive(true);
        nodes.DisableNodes();
        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void ShowSettings()
    {
        uiScreens.SetActive(true);
        settingsScreen.SetActive(true);
        nodes.DisableNodes();
        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void ShowMap()
    {
        uiScreens.SetActive(true);
        worldMap.SetActive(true);
        nodes.DisableNodes();
        AudioManager.instance.Play("GeneralButtonClick");
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
        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void BackToMain()
    {
        AudioManager.instance.Play("GeneralButtonClick");
        Manager.instance.inventory.Save();
        Manager.instance.equipment.Save();
        PlayerPrefs.Save();
        levelLoader.LoadLevel("Main Menu");
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
