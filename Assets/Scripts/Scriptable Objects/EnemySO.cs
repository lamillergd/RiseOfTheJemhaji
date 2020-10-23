using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public enum EnemyID
    {
        //0
        Tutorial,
        //1
        IUL_Tiger
    }
    
    public new string name;
    public EnemyID enemyID;
    public Sprite appearance;
    public int dmg;
    public int maxHealth;
    //For when enemy special attacks are implemented
    //public GameObject specialAttack;
}
