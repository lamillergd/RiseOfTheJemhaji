using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject charCreation;
    public GameObject settings;

    void Start()
    {
        mainMenu.SetActive(true);
        charCreation.SetActive(false);
        settings.SetActive(false);
    }

    public void StartNewGame()
    {
        mainMenu.SetActive(false);
        charCreation.SetActive(true);
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
