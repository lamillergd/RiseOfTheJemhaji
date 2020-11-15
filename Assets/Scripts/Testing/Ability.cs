using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public BasicObjectInfo objectInfo;
    public ClassType classType;
    public bool requiresTarget; //might not need
    public bool canCastOnSelf; //might not need
    public int abilityCooldown; //secs
    //public GameObject particleEffect;
    public int manaCost;
    public int abilityLevel;

    public enum ClassType
    {
        Myconist,
        Botanist,
        Guardian,
        Paladin,
        Necromancer,
        Pyromancer,
        All
    }

    public virtual int UseAbility()
    {
        Debug.Log("Assign Damage Method");
        return 0;
    }
}
