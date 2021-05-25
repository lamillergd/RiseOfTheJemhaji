using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
}
