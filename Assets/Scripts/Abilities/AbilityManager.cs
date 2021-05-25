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

        for (int i = 0; i < Manager.instance.abilities.Count; i++)
        {
            availableAbilities.Add(Instantiate(Manager.instance.abilities[i], Manager.instance.abilities[i].transform.position, Quaternion.identity));
        }

        for (int i = 0; i < availableAbilities.Count; i++)
        {
            AbilityTemplate ability = availableAbilities[i].GetComponent<AbilityTemplate>();
            castButtons[i].image.sprite = ability.icon;
            castButtons[i].GetComponent<CastingButton>().cooldownTime = ability.cooldown;
            castButtons[i].onClick.AddListener(() => { ability.Cast(); });
        }

        foreach (Button button in castButtons)
        {
            if (button.GetComponent<Image>().sprite == null)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}
