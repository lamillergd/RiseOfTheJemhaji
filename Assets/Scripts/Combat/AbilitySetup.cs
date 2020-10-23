using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySetup : MonoBehaviour
{
    [Header("ScriptableObject")]
    public Ability ability;
    public Image artwork;
    public Image cooldownImage;
    public int damageType;
    public int manaCost;
    public float cooldown;
    public int damage;
    public int duration;
    //NOT USING WHILE FINDING TARGET USING GAMEOBJECT.FIND
    //public int noOfTargets;

    [Header("Ability Setup")]
    public GameObject target;
    public GameObject playerObj;
    private Enemy enemy;  
    public GameObject damageEffect;
    private Player player;
    private bool isCooldown = false;
    private bool isFiring = false;

    void Start()
    {
        artwork.sprite = ability.artwork;
        manaCost = ability.manaCost;
        damage = ability.dmg;
        damageType = (int)ability.type;
        cooldown = ability.cooldown;
        duration = ability.duration;
        //noOfTargets = ability.noOfTargets;

        cooldownImage.fillAmount = 0;
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        //FOR TESTING
        target = GameObject.Find("Basic Enemy (TEST)");
        enemy = target.GetComponent<Enemy>();
    }

    void Update()
    {
        if(isFiring)
        {
            OnCooldown();
        }
    }

    public void CastAbility()
    {
        if (player.currentMana >= manaCost && target.activeInHierarchy == true && !isFiring)
        {
            DealDamage();
            player.currentMana -= manaCost;
            isFiring = true;
        }

        if (target.activeInHierarchy == false)
        {
            Debug.Log("No Target");
        }

        if (player.currentMana < manaCost)
        {
            Debug.Log("Mana");
        }

        if (isFiring)
        {
            Debug.Log("On Cooldown");
        }
    }

    public void DealDamage()
    {
        switch (damageType)
        {
            case 0:
                SingleTargetSingleHit();
                break;
            case 1:
                StartCoroutine(SingleTargetDOT());
                break;
            case 2:
                AOESingleHit();
                break;
            case 3:
                AOEDOT();
                break;
            default:
                Debug.Log("broken");
                break;
        }
    }

    void SingleTargetSingleHit()
    {
        enemy.currentHealth -= damage;

        StartCoroutine(changeColour());
        Instantiate(damageEffect, target.transform.position, Quaternion.identity);

        if (enemy.currentHealth <= 0)
        {
            target.SetActive(false);
        }

        enemy.SetSlider(enemy.currentHealth);
    }

    IEnumerator SingleTargetDOT()
    {
        int DOTDamage = damage / duration;
        int countdown = 0;
        while (countdown != duration && enemy.currentHealth > 0)
        {
            yield return new WaitForSeconds(1);
            enemy.currentHealth -= DOTDamage;
            enemy.SetSlider(enemy.currentHealth);
            StartCoroutine(changeColour());
            Instantiate(damageEffect, target.transform.position, Quaternion.identity);
            countdown++;

        }

        if (enemy.currentHealth <= 0)
        {
            target.SetActive(false);
        }
    }

    void AOESingleHit()
    {

    }

    //Needs to be an IEnumarator
    void AOEDOT()
    {

    }

    IEnumerator changeColour()
    {
        SpriteRenderer enemySR = target.GetComponentInChildren<SpriteRenderer>();
        enemySR.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        enemySR.color = Color.white;
    }

    void OnCooldown()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            cooldownImage.fillAmount = 1;
        }

        if (isCooldown)
        {
            cooldownImage.fillAmount -= 1 / cooldown * Time.deltaTime;

            if(cooldownImage.fillAmount <= 0)
            {
                cooldownImage.fillAmount = 0;
                isCooldown = false;
                isFiring = false;
            }
        }
    }
}
