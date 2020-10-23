using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
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
        //FOR TESTING - loading new data when lvl up happens
        if (Input.GetKeyDown(KeyCode.L))
        {
            maxHealth = 200;
            maxMana = 200;
        }
        //    ******

        SavePlayer();
    }

    public void LoadPlayer()
    {
        maxMana = Manager.instance.mana;
        maxHealth = Manager.instance.health;
    }
    
    public void SavePlayer()
    {
        Manager.instance.health = maxHealth;
        Manager.instance.mana = maxMana;
    }
}
