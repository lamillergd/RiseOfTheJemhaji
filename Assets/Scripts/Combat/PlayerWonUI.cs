using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWonUI : MonoBehaviour
{
    public CombatManager combatManager;
    public Text xpText;
    public List<Text> itemsGained;
    public List<Image> itemImages;

    void Start()
    {
        xpText.text = "You have gained " + combatManager.totalXP + "xp";

        for (int i = 0; i < Manager.instance.lootToAdd.Count; i++)
        {
            itemsGained[i].gameObject.SetActive(true);
            itemsGained[i].text = Manager.instance.lootToAdd[i].name;
            itemImages[i].sprite = Manager.instance.lootToAdd[i].uiDisplay;
        }
    }
}
