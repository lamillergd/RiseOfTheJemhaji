using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> availableAbilities;
    public List<Button> featSlots;

    void Start()
    {
        for (int i = 0; i < availableAbilities.Count; i++)
        {
            featSlots[i].gameObject.AddComponent<CallAbility>();
            CallAbility callAbility = featSlots[i].gameObject.GetComponent<CallAbility>();

            callAbility.ability = availableAbilities[i];

            featSlots[i].onClick.AddListener(() =>
            {
                callAbility.Cast();
            });
        }
               
    }
}
