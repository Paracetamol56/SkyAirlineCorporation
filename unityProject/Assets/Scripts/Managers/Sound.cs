using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
    

[System.Serializable]
public class Sound
{

    public AudioClip au_clip;
    public string str_name;

    [Range(0f, 1f)]
    public float f_volume;
    [Range(.1f, 3f)]
    public float f_pitch;

    [HideInInspector]
    public AudioSource au_source;
}
