using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public int health;
    public int mana;

    public GameObject nodeObject;
    public MapNode currentNode;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        health = 100;
        mana = 100;
    }

    void Start()
    {

    }

    void Update()
    {
        if (nodeObject != null)
        {
            currentNode = nodeObject.GetComponent<MapNode>();
            nodeObject.SetActive(false);
        }
    }
}
