using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject charCreation;
    public GameObject settings;
    public Button newGame;
    public Button continueGame;

    void Start()
    {
        mainMenu.SetActive(true);
        charCreation.SetActive(false);
        settings.SetActive(false);
        SetButtons();
    }

    void SetButtons()
    {
        newGame.interactable = true;
        continueGame.interactable = false;

        if (File.Exists(string.Concat(Application.persistentDataPath, Manager.instance.inventory.savePath)))
        {
            continueGame.interactable = true;
        }
    }

    public void StartNewGame()
    {
        mainMenu.SetActive(false);
        charCreation.SetActive(true);

        File.Delete(string.Concat(Application.persistentDataPath, Manager.instance.inventory.savePath));
        Manager.instance.inventory.container.Clear();
    }

    public void LoadGame()
    {
        Manager.instance.inventory.Load();
    }

    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseAndSaveSettings()
    {
        //Save settings on screen close
        Debug.Log("Saved settings");
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }
}
