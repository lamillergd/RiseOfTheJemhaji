using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNodes : MonoBehaviour
{
    public List<GameObject> mapNodes = new List<GameObject>();
    public GameObject uiScreens;

    void Update()
    {
        if (uiScreens.activeInHierarchy == true)
        {
            foreach (GameObject n in mapNodes)
            {
                n.GetComponent<CircleCollider2D>().enabled = false;
            }
        }

        if (uiScreens.activeInHierarchy == false)
        {
            foreach (GameObject n in mapNodes)
            {
                n.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }
}
