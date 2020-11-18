using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public GameObject playerObj;
    public Player player;
    public List<GameObject> availableAbilities;
    public List<Button> castButtons;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        for (int i = 0; i < player.availableAbilties.Count; i++)
        {
            availableAbilities.Add(Instantiate(player.availableAbilties[i], player.availableAbilties[i].transform.position, Quaternion.identity));
        }

        for (int i = 0; i < availableAbilities.Count; i++)
        {
            AbilityTemplate ability = availableAbilities[i].GetComponent<AbilityTemplate>();
            castButtons[i].image.sprite = ability.abilitySO.icon;
            castButtons[i].GetComponent<CastingButton>().cooldownTime = ability.abilitySO.cooldown;
            castButtons[i].onClick.AddListener(() => { ability.Cast(); });
        }
    }

    
    void Update()
    {
        
    }
}
