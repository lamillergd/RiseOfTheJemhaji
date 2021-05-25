using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Gains")]
    public int xp;
    public List<ItemSO> lootTable = new List<ItemSO>();

    [Header("General")]
    public int level;
    public int damage;
    public int scaledDamage;
    public int maxHealth;
    public int currentHealth;
    public Slider healthSlider;
    public GameObject playerObj;
    public string soundToPlay;

    float attackTimer;
    float cooldown;

    void Start()
    {
        scaledDamage = damage * level;
        currentHealth = maxHealth;
        SetSliderMax(maxHealth);
        attackTimer = 3f;
        cooldown = 5f;

        playerObj = GameObject.Find("Player Object");
    }

    void Update()
    {
        if (playerObj.activeInHierarchy)
        {
            CooldownTimer();
        }
        else
        {
            Debug.Log("No player");
        }
    }

    public void CooldownTimer()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if (attackTimer < 0)
        {
            attackTimer = 0f;
        }
        if (attackTimer == 0)
        {
            DealDamage();
            attackTimer = cooldown;
        }
    }
    
    public void SetSliderMax(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetSlider(int health)
    {
        healthSlider.value = health;
    }

    public void DealDamage()
    {
        Player.instance.currentHealth -= scaledDamage;
        StartCoroutine(changeColour());
        if (Manager.instance.isFemale == true)
        {
            AudioManager.instance.Play("DamageFemale");
        }
        else
        {
            AudioManager.instance.Play("DamageMale");
        }

        if (Player.instance.currentHealth <= 0)
        {
            playerObj.SetActive(false);
        }

        ScoreSystem.instance.SetHealthSlider(Player.instance.currentHealth);
    }

    IEnumerator changeColour()
    {
        SpriteRenderer playerSR = playerObj.GetComponentInChildren<SpriteRenderer>();
        playerSR.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        playerSR.color = Color.white;
    }
}
