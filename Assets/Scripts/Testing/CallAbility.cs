using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAbility : MonoBehaviour
{
    public Ability ability;

    public void Cast()
    {
        ability.UseAbility();
    }
}
