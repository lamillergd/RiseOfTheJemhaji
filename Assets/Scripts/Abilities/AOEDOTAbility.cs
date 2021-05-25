using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDOTAbility : AbilityTemplate
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
            StartCoroutine(DealDamageAOEDOT());
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

    IEnumerator DealDamageAOEDOT()
    {
        foreach (GameObject go in targetList)
        {
            Enemy target = go.GetComponent<Enemy>();

            int DOTDamage = dmg / duration;
            int countdown = 0;

            while (countdown != duration && target.currentHealth > 0)
            {
                yield return new WaitForSeconds(1);
                target.currentHealth -= DOTDamage;

                StartCoroutine(changeEnemyColour(target));
                target.SetSlider(currentTarget.currentHealth);
                countdown++;
            }

            if (target.currentHealth <= 0)
            {
                AudioManager.instance.Play(target.soundToPlay);
                target.gameObject.SetActive(false);
            }
        }
    }
}
