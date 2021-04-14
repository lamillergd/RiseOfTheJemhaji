using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Safezone", menuName = "Safezone")]
public class SafezoneSO : ScriptableObject
{
    public Locations location;
    public Sprite background;
    public bool hasHerbalism;
    public bool hasLogging;
    public bool hasFishing;
    public bool hasMining;
    public GameObject[] NPC;
}
