using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNodes : MonoBehaviour
{
    public List<GameObject> mapNodes = new List<GameObject>();
    public GameObject uiScreens;
    public GameObject safeZone;

    public void DisableNodes()
    {
        foreach (GameObject n in mapNodes)
        {
            n.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void EnableNodes()
    {
        foreach (GameObject n in mapNodes)
        {
            n.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
