using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/New Enemy")]
public class EnemySO : ScriptableObject
{
    public new string name;
    public int ID;
    public Sprite appearance;
    public int dmg;
    public int maxHealth;
    //For when enemy special attacks are implemented
    //public GameObject specialAttack;
}
