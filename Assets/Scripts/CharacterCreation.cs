using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public bool classPicked;
    public bool namePicked;
    public bool appearancePicked;
    public string playerAppearance;

    public Image classVisual;
    public Text classNameText;
    public Text playerNameText;

    public LevelLoader levelLoader;

    void Start()
    {
        classPicked = false;
        namePicked = false;
        appearancePicked = false;
    }

    public void ClassSelect(Sprite classImage)
    {
        classVisual.sprite = classImage;
        classPicked = true;
    }

    public void ClassName(string className)
    {
        classNameText.text = className;
    }

    public void SavePlayerName()
    {
        namePicked = true;
    }

    public void AppearanceSelect(string appearance)
    {
        Debug.Log(appearance);
        playerAppearance = appearance;
        appearancePicked = true;
    }

    public void ConfirmSelection()
    {
        if (classPicked && namePicked && appearancePicked)
        {
            SaveData();
            levelLoader.LoadLevel("Tutorial");
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Creation not complete");
        }
    }

    void SaveData()
    {
        Manager.instance.playerName = playerNameText.text;
        Manager.instance.playerClass = classNameText.text;
        Manager.instance.appearance = playerAppearance;
    }
}
