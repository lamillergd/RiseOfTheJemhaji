using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTemplate : MonoBehaviour
{
    [Header("Base Information")]
    public string abilityName;
    public PlayerClasses classType;
    public int level;
    public Sprite icon;
    public int manaCost;
    public int baseX;
    public int cooldown;
    public int duration;
    public int targets;
    public Color damageColour;
    
    public void Init()
    {
        level = Manager.instance.level;
    }

    public void SetDamageColour()
    {
        //set damagecolour depending on classType
    }

    public virtual void Cast()
    {
        Debug.Log("do a thing");
    }

    public IEnumerator changeEnemyColour(Enemy target)
    {
        SpriteRenderer enemySR = target.GetComponentInChildren<SpriteRenderer>();
        enemySR.color = damageColour;
        yield return new WaitForSeconds(0.25f);
        enemySR.color = Color.white;
    }
}
