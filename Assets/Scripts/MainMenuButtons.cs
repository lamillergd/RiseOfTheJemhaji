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
    public Slider volumeSlider;

    void Start()
    {
        mainMenu.SetActive(true);
        charCreation.SetActive(false);
        settings.SetActive(false);
        volumeSlider.value = AudioManager.instance.volume;
        SetButtons();
    }

    void SetButtons()
    {
        newGame.interactable = true;
        continueGame.interactable = false;

        if (PlayerPrefs.HasKey("fullBody"))
        {
            continueGame.interactable = true;
        }
    }

    public void StartNewGame()
    {
        mainMenu.SetActive(false);
        charCreation.SetActive(true);

        File.Delete(string.Concat(Application.persistentDataPath, Manager.instance.inventory.savePath));
        File.Delete(string.Concat(Application.persistentDataPath, Manager.instance.equipment.savePath));
        PlayerPrefs.DeleteKey("fullBody");
        PlayerPrefs.DeleteKey("headshot");
        PlayerPrefs.DeleteKey("Node Progress");

        Manager.instance.inventory.Clear();
        Manager.instance.equipment.Clear();

        AudioManager.instance.Play("NewGameButton");
    }

    public void LoadGame()
    {
        Manager.instance.inventory.Load();
        Manager.instance.equipment.Load();

        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);

        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void CloseAndSaveSettings()
    {
        //Save settings on screen close
        Debug.Log("Saved settings");
        mainMenu.SetActive(true);
        settings.SetActive(false);

        AudioManager.instance.Play("GeneralButtonClick");
    }
}
