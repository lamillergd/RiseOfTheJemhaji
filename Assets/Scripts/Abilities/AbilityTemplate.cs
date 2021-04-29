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
        SetDamageColour();
    }

    public void SetDamageColour()
    {
        switch (classType)
        {
            case PlayerClasses.Myconist:
                damageColour = new Color32(136,18,255,255);
                break;
            case PlayerClasses.Botanist:
                damageColour = new Color32(42,178,39,255);
                break;
            case PlayerClasses.Guardian:
                damageColour = new Color32(84,81,178,255);
                break;
            case PlayerClasses.Paladin:
                damageColour = new Color32(219,190,67,255);
                break;
            case PlayerClasses.Pyromancer:
                damageColour = new Color32(209,93,58,255);
                break;
            case PlayerClasses.Necromancer:
                damageColour = new Color32(87,87,87,255);
                break;
            default:
                damageColour = Color.black;
                break;
        }
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
