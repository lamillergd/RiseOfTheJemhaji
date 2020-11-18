using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTAbility : AbilityTemplate
{
    [Header("Ability Specific")]
    public int lvl;
    public int dmg;
    public List<GameObject> targetList;
    Enemy currentTarget;
    public GameObject combatManagerObj;
    CombatManager combatManager;
    public GameObject effect;

    void Start()
    {
        InitSO();
        dmg = baseX * lvl;
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
            StartCoroutine(DealDamageDOT());
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

    IEnumerator DealDamageDOT()
    {
        int DOTDamage = dmg / duration;
        int countdown = 0;
        while (countdown != duration && currentTarget.currentHealth > 0)
        {
            yield return new WaitForSeconds(1);
            currentTarget.currentHealth -= DOTDamage;
            currentTarget.SetSlider(currentTarget.currentHealth);
            StartCoroutine(changeEnemyColour(currentTarget));
            Instantiate(effect, currentTarget.transform.position, Quaternion.identity);
            countdown++;
        }

        if (currentTarget.currentHealth <= 0)
        {
            currentTarget.gameObject.SetActive(false);
        }
    }
}
