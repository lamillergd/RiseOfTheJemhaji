using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;
    public Text manaText;
    public Text healthText;
    public Slider ManaSlider;
    public Slider HealthSlider;

    void Awake()
    {
        instance = this;
        SetSliders();
    }

    public void AddMana(int same, int numberInMatch, bool isMain)
    {
        if (same == numberInMatch && isMain)
        {
            Player.instance.currentMana += (numberInMatch - 1);
        }
    }

    public void SetManaSlider(int mana)
    {
        ManaSlider.value = mana;
    }

    public void SetMaxManaSlider (int mana)
    {
        ManaSlider.maxValue = mana;
    }

    public void SetHealthSlider (int health)
    {
        HealthSlider.value = health;
    }

    public void SetMaxHealthSlider (int health)
    {
        HealthSlider.maxValue = health;
        HealthSlider.value = health;
    }

    public void TakeDamage(int dmg)
    {
        Player.instance.currentHealth -= dmg;
        SetHealthSlider(Player.instance.currentHealth);
    }

    public void SetSliders()
    {
        SetMaxManaSlider(Player.instance.maxMana);
        SetManaSlider(Player.instance.currentMana);
        SetMaxHealthSlider(Player.instance.maxHealth);
        SetHealthSlider(Player.instance.currentHealth);
    }

    void Update()
    {
        manaText.text = Player.instance.currentMana + " / " + Player.instance.maxMana;
        healthText.text = Player.instance.currentHealth + " / " + Player.instance.maxHealth;
        SetSliders();

        if (Player.instance.currentMana > Player.instance.maxMana)
        {
            Player.instance.currentMana = Player.instance.maxMana;
        }

        if (Player.instance.currentHealth < 0)
        {
            Player.instance.currentHealth = 0;
        }

        //FOR TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
        //    ******
    }
}
