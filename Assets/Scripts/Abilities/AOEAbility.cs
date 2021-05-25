using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAbility : AbilityTemplate
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

    public override void Cast()
    {
        if (Player.instance.currentMana >= manaCost && currentTarget.gameObject.activeInHierarchy == true)
        {
            DealDamageAOE();
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

    void DealDamageAOE()
    {
        foreach (GameObject go in targetList)
        {
            Enemy target = go.GetComponent<Enemy>();

            target.currentHealth -= dmg;
            StartCoroutine(changeEnemyColour(target));

            if (target.currentHealth <= 0)
            {
                AudioManager.instance.Play(target.soundToPlay);
                target.gameObject.SetActive(false);
            }

            target.SetSlider(currentTarget.currentHealth);
        }
    }
}
