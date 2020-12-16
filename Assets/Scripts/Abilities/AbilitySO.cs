using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/New Ability")]
public class AbilitySO : ScriptableObject
{  
    public new string name;
    //public string descritpion;
    public Class classType;
    public Sprite icon;
    //public int level;
    public int manaCost;
    public int x;
    public int y;
    public int z;
    public int cooldown;
    public int duration;
    public int noOfTargets;

    public enum Class
    {
        Botanist,
        Myconist,
        Guardian,
        Paladin,
        Pyromancer,
        Necromancer,
        All
    }
}
