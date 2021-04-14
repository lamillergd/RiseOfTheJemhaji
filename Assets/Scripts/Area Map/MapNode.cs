using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public enum NodeType
    {
        Combat,
        Safe,
        Portal,
        Telepad
    }

    public int nodeID;
    public bool isUnlocked;
    public GameObject unlockedImage;
    public string sceneName;
    public SafezoneSO safezoneData;
    public NodeType nodeType;
    public List<Sprite> nodeImage = new List<Sprite>();
    public MapNode nodeAfterTelepad;
    SpriteRenderer sr;

    [Header("Level/Scene Loading")]
    public GameObject levelLoaderObj;
    LevelLoader levelLoader;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        levelLoader = levelLoaderObj.GetComponent<LevelLoader>();
        switch (nodeType)
        {
            case NodeType.Combat:
                sr.sprite = nodeImage[0];
                break;

            case NodeType.Safe:
                sr.sprite = nodeImage[1];
                break;

            case NodeType.Portal:
                sr.sprite = nodeImage[2];
                break;

            case NodeType.Telepad:
                sr.sprite = nodeImage[3];
                break;

            default:
                sr.sprite = null;
                Debug.Log("No type selected");
                break;
        }

        UpdateNode();
    }

    void UpdateNode()
    {
        if (nodeID <= PlayerPrefs.GetInt("Node Progress"))
        {
            isUnlocked = true;
        }

        if (isUnlocked == true)
        {
            unlockedImage.SetActive(false);
        }
        if (isUnlocked == false)
        {
            unlockedImage.SetActive(true);
        }
    }

    void UnlockAfterTelepad()
    {
        nodeAfterTelepad.UpdateNode();
    }

    void LoadScene()
    {
        if (sceneName == "None")
        {
            Debug.Log("No scene to load!");
        }
        else
        {
            levelLoader.LoadLevel(sceneName);
        }
    }

    void OnMouseDown()
    {
        if (isUnlocked)
        {
            switch (nodeType)
            {
                case NodeType.Combat:
                    Manager.instance.currentNodeID = nodeID;
                    LoadScene();
                    break;

                case NodeType.Safe:
                    Manager.instance.currentSafezone = this.safezoneData;
                    Manager.instance.CheckProgress(nodeID);
                    LoadScene();
                    break;

                case NodeType.Portal:
                    Debug.Log("Travelling to next map");
                    break;

                case NodeType.Telepad:
                    Debug.Log("You have discovered a place");
                    Manager.instance.CheckProgress(nodeID);
                    UnlockAfterTelepad();
                    break;

                default:
                    break;
            }
        }
        else
        {
            Debug.Log("Not unlocked");
        }
    }
}
