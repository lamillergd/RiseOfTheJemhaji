using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; set; }

    public Safezone safezone;
    public GameObject dialogueScreen;
    public string npcName;
    public List<string> currentDialogue = new List<string>();

    public Image npcImage;
    public Text nameText;
    public Text dialogueText;
    public Button continueDialogueButton;
    int dialogueIndex;

    void Awake()
    {
        continueDialogueButton.onClick.AddListener(delegate { ContinueDialogue(); });

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        currentDialogue = new List<string>(lines.Length);
        currentDialogue.AddRange(lines);
        this.npcName = npcName;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = currentDialogue[dialogueIndex];
        nameText.text = npcName;
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < currentDialogue.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = currentDialogue[dialogueIndex];
        }
        else
        {
            safezone.EnableScreens();
        }
    }
}
