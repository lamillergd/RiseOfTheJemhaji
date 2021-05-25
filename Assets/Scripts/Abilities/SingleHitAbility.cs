using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHitAbility : AbilityTemplate
{
    [Header("Ability Specific")]
    public int dmg;
    public List<GameObject> targetList;
    Enemy currentTarget;
    public GameObject combatManagerObj;
    CombatManager combatManager;

    void Start()
    {
        Init();
        dmg = baseX * level;
        combatManagerObj = GameObject.Find("CombatManager");
        combatManager = combatManagerObj.GetComponent<CombatManager>();
        targetList = combatManager.activeEnemies;
    }
    
    void Update()
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].activeInHierarchy)
            {
                currentTarget = targetList[i].GetComponent<Enemy>();
            }
        }
    }

    public override void Cast()
    {
        if (Player.instance.currentMana >= manaCost && currentTarget.gameObject.activeInHierarchy == true)
        {
            DealDamageSingle();
            AudioManager.instance.Play(damageSoundName);
            Player.instance.currentMana -= manaCost;
        }

        if (currentTarget.gameObject.activeInHierarchy == false)
        {
            Debug.Log("no target");
        }

        if (Player.instance.currentMana < manaCost)
        {
            Debug.Log("mana");
        }
    }

    void DealDamageSingle()
    {
        currentTarget.currentHealth -= dmg;
        StartCoroutine(changeEnemyColour(currentTarget));

        if (currentTarget.currentHealth <= 0)
        {
            AudioManager.instance.Play(currentTarget.soundToPlay);
            currentTarget.gameObject.SetActive(false);
        }

        currentTarget.SetSlider(currentTarget.currentHealth);
    }
}
