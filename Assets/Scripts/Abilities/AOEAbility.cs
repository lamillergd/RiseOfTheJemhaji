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
    public GameObject effect;

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
            Instantiate(effect, target.transform.position, Quaternion.identity);

            if (target.currentHealth <= 0)
            {
                target.gameObject.SetActive(false);
            }

            target.SetSlider(currentTarget.currentHealth);
        }
    }
}
