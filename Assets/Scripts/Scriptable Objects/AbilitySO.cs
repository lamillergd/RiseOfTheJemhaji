using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Damage Ability", menuName = "Abilities/Damage Ability")]
public class AbilitySO : ScriptableObject
{
    public enum DamageAbilityType
    {
        SingleTarget_SingleHit,
        SingleTarget_DOT,
        AOE_SingleHit,
        AOE_DOT
    }
    
    [Header("General")]
    public new string name;
    public string descritpion;
    //will be able to get rid of below line
    public DamageAbilityType type;

    public Sprite artwork;
    public Image cooldownImage;

    public int manaCost;
    public int x;
    public int y;
    public int z;
    public int cooldown;

    //will be able to get rid of below line
    [Header("Damage Over Time ONLY")]
    public int duration;

    [Header("Area Of Effect")]
    public int noOfTargets;
}
