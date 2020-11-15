using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenStrike : Ability
{
    [Header("Ability Specific")]
    int xValue = 5;

    void Start()
    {
        xValue *= abilityLevel;
        objectInfo.objName = "Green Strike";
        objectInfo.objDescription = "Deal " + xValue + " to your target.";
    }

    public override int UseAbility()
    {
        Debug.Log(xValue);
        return xValue;
    }

}
