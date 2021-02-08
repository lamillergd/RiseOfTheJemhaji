using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public bool classPicked;
    public bool namePicked;
    //will be changed to hold the sprite
    public bool appearancePicked;
    public string playerAppearance;
    public PlayerClasses playerClass;

    public Image classVisual;
    public Text classNameText;
    public Text playerNameText;

    [Header("Starting Items")]
    public List<ItemSO> myconistEquipment = new List<ItemSO>();
    public List<ItemSO> botanistEquipment = new List<ItemSO>();

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
        switch (className)
        {
            case "Myconist":
                playerClass = PlayerClasses.Myconist;
                break;
            case "Botanist":
                playerClass = PlayerClasses.Botanist;
                break;
            case "Guardian":
                playerClass = PlayerClasses.Guardian;
                break;
            case "Paladin":
                playerClass = PlayerClasses.Paladin;
                break;
            case "Pyromancer":
                playerClass = PlayerClasses.Pyromancer;
                break;
            case "Necromancer":
                playerClass = PlayerClasses.Necromancer;
                break;
            default:
                break;
        }
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
            SetEquipment();
            SaveData();
            levelLoader.LoadLevel("Tutorial");
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Creation not complete");
        }
    }

    void SetEquipment()
    {
        List<ItemSO> equipToAdd = new List<ItemSO>();

        switch (playerClass)
        {
            case PlayerClasses.Myconist:
                equipToAdd = myconistEquipment;
                break;

            case PlayerClasses.Botanist:
                equipToAdd = botanistEquipment;
                break;

            case PlayerClasses.Guardian:
                break;
            case PlayerClasses.Paladin:
                break;
            case PlayerClasses.Pyromancer:
                break;
            case PlayerClasses.Necromancer:
                break;
            default:
                break;
        }

        for (int i = 0; i < equipToAdd.Count; i++)
        {
            Manager.instance.equipment.AddItem(new Item(equipToAdd[i]), 1);
        }
    }

    void SaveData()
    {
        Manager.instance.playerName = playerNameText.text;
        Manager.instance.playerClass = playerClass;
        Manager.instance.appearance = playerAppearance;
    }
}

public enum PlayerClasses
{
    Myconist,
    Botanist,
    Guardian,
    Paladin,
    Pyromancer,
    Necromancer
}
