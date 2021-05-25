using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public GameObject characters;
    public bool classPicked;
    public bool namePicked;
    public bool appearancePicked;
    public GameObject classSelection;
    public GameObject nameSelection;
    public GameObject appearanceSelection;
    public GameObject continueButton;
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
        classSelection.gameObject.SetActive(true);
        nameSelection.gameObject.SetActive(false);
        appearanceSelection.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (classPicked)
        {
            nameSelection.gameObject.SetActive(true);
        }
        if (namePicked)
        {
            appearanceSelection.gameObject.SetActive(true);
        }
        if (appearancePicked)
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    public void ClassSelect(Sprite classImage)
    {
        classVisual.sprite = classImage;
        classPicked = true;

        AudioManager.instance.Play("GeneralButtonClick");
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
        switch (playerClass)
        {
            case PlayerClasses.Myconist:
                if (appearance == "Female")
                {
                    SetSprites(0);
                }
                if (appearance == "Male")
                {
                    SetSprites(1);
                }
                break;

            case PlayerClasses.Botanist:
                //change once art complete
                if (appearance == "Female")
                {
                    SetSprites(0);
                }
                if (appearance == "Male")
                {
                    SetSprites(1);
                }
                break;

            case PlayerClasses.Guardian:
                //change once art complete
                if (appearance == "Female")
                {
                    SetSprites(0);
                }
                if (appearance == "Male")
                {
                    SetSprites(1);
                }
                break;

            case PlayerClasses.Paladin:
                //change once art complete
                if (appearance == "Female")
                {
                    SetSprites(0);
                }
                if (appearance == "Male")
                {
                    SetSprites(1);
                }
                break;

            case PlayerClasses.Pyromancer:
                //change once art complete
                if (appearance == "Female")
                {
                    SetSprites(0);
                }
                if (appearance == "Male")
                {
                    SetSprites(1);
                }
                break;

            case PlayerClasses.Necromancer:
                //change once art complete
                if (appearance == "Female")
                {
                    SetSprites(0);
                }
                if (appearance == "Male")
                {
                    SetSprites(1);
                }
                break;

            default:
                break;
        }

        if (appearance == "Female")
        {
            Manager.instance.isFemale = true;
        }
        else
        {
            Manager.instance.isFemale = false;
        }

        appearancePicked = true;

        AudioManager.instance.Play("GeneralButtonClick");
    }

    void SetSprites(int index)
    {
        classVisual.sprite = characters.GetComponent<Characters>().fullBodies[index];
        PlayerPrefs.SetInt("fullBody", index);
        PlayerPrefs.SetInt("headshot", index);
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

        AudioManager.instance.Play("NewGameButton");
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
        Manager.instance.fullBody = classVisual.sprite;
        Manager.instance.headshot = characters.GetComponent<Characters>().headshots[PlayerPrefs.GetInt("headshot")];
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
