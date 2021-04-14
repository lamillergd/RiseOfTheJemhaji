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
