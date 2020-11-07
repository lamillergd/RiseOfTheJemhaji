using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject charCreation;

    void Start()
    {
        mainMenu.SetActive(true);
        charCreation.SetActive(false);
    }

    public void StartNewGame()
    {
        mainMenu.SetActive(false);
        charCreation.SetActive(true);
    }
}
