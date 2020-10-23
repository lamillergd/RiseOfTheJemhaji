using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Damage Ability", menuName = "Abilities/Damage Ability")]
public class Ability : ScriptableObject
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
    public DamageAbilityType type;

    public Sprite artwork;
    public Image cooldownImage;

    public int manaCost;
    public int dmg;
    public int cooldown;

    [Header("Damage Over Time ONLY")]
    public int duration;

    [Header("Area Of Effect")]
    public int noOfTargets;
}
