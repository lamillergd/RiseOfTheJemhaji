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

    public bool isUnlocked;
    public GameObject unlockedImage;
    public string sceneName;
    public NodeType nodeType;
    public List<Sprite> nodeImage = new List<Sprite>();
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        switch ((int)nodeType)
        {
            case 0:
                sr.sprite = null;
                Debug.Log("No type selected");
                break;
            case 1:
                sr.sprite = nodeImage[0];
                break;
            case 2:
                sr.sprite = nodeImage[1];
                break;
            case 3:
                sr.sprite = nodeImage[2];
                break;
            default:
                sr.sprite = null;
                Debug.Log("No type selected");
                break;
        }

        if (isUnlocked)
        {
            unlockedImage.SetActive(false);
        }
        if (!isUnlocked)
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
                //Change to ASync load
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("Not unlocked");
            }
        }
        
    }
}
