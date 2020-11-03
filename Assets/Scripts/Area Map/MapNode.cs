using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapNode : MonoBehaviour
{
    public enum NodeType
    {
        None,
        Combat,
        Safe,
        Portal
    }

    public enum Map
    {
        Tutorial,
        Iulara,
        Tychis
    }
    
    public bool isUnlocked;
    public bool isCompleted;
    public GameObject unlockedImage;
    public string sceneName;
    public int nodeNumber;
    public NodeType nodeType;
    public List<Sprite> nodeImage = new List<Sprite>();
    public List<MapNode> unlockWhenCompleted = new List<MapNode>();
    SpriteRenderer sr;

    public GameObject levelLoaderObj;
    LevelLoader levelLoader;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        levelLoader = levelLoaderObj.GetComponent<LevelLoader>();
        switch ((int)nodeType)
        {
            case 0:
                sr.sprite = null;
                Debug.Log("No type selected");
                break;
            //Combat Node
            case 1:
                sr.sprite = nodeImage[0];
                break;
            //Safe Node
            case 2:
                sr.sprite = nodeImage[1];
                isCompleted = true;
                isUnlocked = true;
                break;
            //Portal Node
            case 3:
                sr.sprite = nodeImage[2];
                break;
            //Need To Add A Telepad Node
            default:
                sr.sprite = null;
                Debug.Log("No type selected");
                break;
        }

        if (Manager.instance.nodeObject != null)
        {
            if (Manager.instance.nodeObject.name == this.gameObject.name)
            {
                isCompleted = Manager.instance.currentNode.isCompleted;
                Destroy(Manager.instance.nodeObject.gameObject);
                Manager.instance.nodeObject = null;
                Manager.instance.currentNode = null;
            }
        }
    }

    void Update()
    {
        if (isCompleted == true)
        {
            if (unlockWhenCompleted != null)
            {
                for (int i = 0; i < unlockWhenCompleted.Count; i++)
                {
                    unlockWhenCompleted[i].isUnlocked = true;
                }
            }
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

    void OnMouseDown()
    {
        if (sceneName == "None")
        {
            Debug.Log("no scene selected");
        }
        else
        {
            if (isUnlocked)
            {
                Manager.instance.nodeObject = this.gameObject;
                DontDestroyOnLoad(Manager.instance.nodeObject);
                levelLoader.LoadLevel(sceneName);
            }
            else
            {
                Debug.Log("Not unlocked");
            }
        }
        
    }
}
