using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Button startDialogue;
    public string npcName;
    [TextArea(5,5)]
    public string[] dialogue;

    void Start()
    {
        startDialogue.onClick.AddListener(delegate { DialogueManager.instance.safezone.EnableDialogue(); });
    }
    
    void Update()
    {
    }

    public void Interact()
    {
        DialogueManager.instance.AddNewDialogue(dialogue, npcName);
        DialogueManager.instance.npcImage.sprite = transform.Find("Mask").GetChild(0).GetComponent<Image>().sprite;
    }
}
