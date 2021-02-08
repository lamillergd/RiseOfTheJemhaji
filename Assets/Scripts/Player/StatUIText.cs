using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUIText : MonoBehaviour
{
    public PlayerStats stats;
    public Text damage;
    public Text mastery;
    public Text armour;
    public Text health;
    public Text mana;
    public Text cooldown;

    void Start()
    {
        SetText();
    }

    void Update()
    {
        SetText();
    }

    public void SetText()
    {
        for (int i = 0; i < stats.attributes.Length; i++)
        {
            switch (stats.attributes[i].type)
            {
                case Attributes.Damage:
                    damage.text = (string.Concat("Damage: ", stats.attributes[i].value.ModifiedValue));
                    break;
                case Attributes.Mastery:
                    mastery.text = (string.Concat("Mastery: ", stats.attributes[i].value.ModifiedValue));
                    break;
                case Attributes.Armour:
                    armour.text = (string.Concat("Armour: ", stats.attributes[i].value.ModifiedValue));
                    break;
                case Attributes.Health:
                    health.text = (string.Concat("Health: ", stats.attributes[i].value.ModifiedValue));
                    break;
                case Attributes.Mana:
                    mana.text = (string.Concat("Mana: ", stats.attributes[i].value.ModifiedValue));
                    break;
                case Attributes.Cooldown_Rate:
                    cooldown.text = (string.Concat("Cooldown Rate: ", stats.attributes[i].value.ModifiedValue));
                    break;
                default:
                    break;
            }
        }
    }
}
