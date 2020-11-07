using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public string playerClass;
    public string playerAppearance;
    public int maxMana;
    public int currentMana;
    public int maxHealth;
    public int currentHealth;
    public GameObject playerImage;

    [SerializeField]
    private GameObject[] featPrefabs;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadPlayer();
        currentMana = 0;
        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    public void LoadPlayer()
    {
        maxMana = Manager.instance.mana;
        maxHealth = Manager.instance.health;
        playerClass = Manager.instance.playerClass;
        playerAppearance = Manager.instance.appearance;
    }
    
    public void SavePlayer()
    {
        Manager.instance.health = maxHealth;
        Manager.instance.mana = maxMana;
    }
}
