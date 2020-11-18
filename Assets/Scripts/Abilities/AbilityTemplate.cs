using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTemplate : MonoBehaviour
{
    [Header("ScriptableObject")]
    public AbilitySO abilitySO;
    public string abilityName;
    public string classType;
    public Sprite icon;
    public int manaCost;
    public int baseX;
    public int baseY;
    public int baseZ;
    public int cooldown;
    public int duration;
    public int targets;
    
    public void InitSO()
    {
        abilityName = abilitySO.name;
        classType = abilitySO.classType.ToString();
        icon = abilitySO.icon;
        manaCost = abilitySO.manaCost;
        baseX = abilitySO.x;
        baseY = abilitySO.y;
        baseZ = abilitySO.z;
        cooldown = abilitySO.cooldown;
        duration = abilitySO.duration;
        targets = abilitySO.noOfTargets;
    }

    public virtual void Cast()
    {
        Debug.Log("do a thing");
    }

    public IEnumerator changeEnemyColour(Enemy target)
    {
        SpriteRenderer enemySR = target.GetComponentInChildren<SpriteRenderer>();
        enemySR.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        enemySR.color = Color.white;
    }
}
