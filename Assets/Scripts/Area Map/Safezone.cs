using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Safezone : MonoBehaviour
{
    public SafezoneSO currentData;
    public LevelLoader levelLoader;
    public string sceneToLoad;
    public GameObject background;
    public GameObject menu;
    public GameObject dialogueScreen;
    public GameObject gatheringScreen;
    public GameObject closeButton;
    public GameObject[] currentNpcs;
    public Transform[] npcsSpawns; 
    public GameObject fruitButton;
    public GameObject mushroomButton;
    public GameObject woodButton;
    public GameObject fishButton;
    public GameObject oreButton;

    void Start()
    {
        currentData = Manager.instance.currentSafezone;
        background.GetComponent<Image>().sprite = currentData.background;
        currentNpcs = currentData.NPC;
        SetButtons();
        SetNPCs();
        EnableScreens();
    }

    public void EnableScreens()
    {
        menu.SetActive(true);
        closeButton.SetActive(true);
        dialogueScreen.SetActive(false);
        gatheringScreen.SetActive(false);
    }

    public void EnableDialogue()
    {
        menu.SetActive(false);
        closeButton.SetActive(false);
        dialogueScreen.SetActive(true);
        AudioManager.instance.Play("GeneralButtonClick");
    }

    public void EnableGathering()
    {
        menu.SetActive(false);
        closeButton.SetActive(false);
        gatheringScreen.SetActive(true);
    }

    void SetButtons()
    {
        fruitButton.SetActive(currentData.hasHerbalism);
        mushroomButton.SetActive(currentData.hasHerbalism);
        woodButton.SetActive(currentData.hasLogging);
        fishButton.SetActive(currentData.hasFishing);
        oreButton.SetActive(currentData.hasMining);
    }

    void SetNPCs()
    {
        for (int i = 0; i < currentNpcs.Length; i++)
        {
            Instantiate(currentNpcs[i], npcsSpawns[i].transform.position, Quaternion.identity, npcsSpawns[i].transform);
        }
    }

    public void StartGathering(string item)
    {
        Debug.Log("Started gathering " + item);
    }

    public void GoBack()
    {
        levelLoader.LoadLevel(sceneToLoad);
        AudioManager.instance.Play("GeneralButtonClick");
    }

    void Update()
    {
        
    }
}
