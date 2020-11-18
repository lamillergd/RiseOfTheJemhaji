using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateButton : MonoBehaviour
{
    public GameObject button;
    public GameObject spawn;

    void Start()
    {
        Instantiate(button, spawn.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
