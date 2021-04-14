using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{
    public GameObject playerPortrait;
    public Sprite[] fullBodies;
    public Sprite[] headshots;

    void Start()
    {
        if (PlayerPrefs.HasKey("fullBody"))
        {
            Manager.instance.fullBody = fullBodies[PlayerPrefs.GetInt("fullBody")];
            Manager.instance.headshot = headshots[PlayerPrefs.GetInt("headshot")];
            playerPortrait.GetComponent<Image>().sprite = Manager.instance.fullBody;
            playerPortrait.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            playerPortrait.GetComponent<Image>().sprite = null;
            playerPortrait.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
}
