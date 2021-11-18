using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;


    public Sound[] au_ListSounds;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        foreach (Sound sound in au_ListSounds)
        {
            sound.au_source = gameObject.GetComponent<AudioSource>();
            sound.au_source.clip = sound.au_clip;
            sound.au_source.pitch = sound.f_pitch;
            sound.au_source.volume = sound.f_volume;

        }
    }

    public void PlayAMusic(string name)
    {

        foreach (Sound sound in au_ListSounds)
        {
            if (sound.str_name == name)
            {
                if (!sound.au_source.isPlaying)
                {
                    sound.au_source.Play();
                    sound.au_source.loop = true;
                }
            }
        }

    }

    public void StopASong(string name)
    {
        foreach (Sound sound in au_ListSounds)
        {
            if (sound.str_name == name)
            {
                sound.au_source.Stop();
            }
        }
    }

    public void PlayASound(string name)
    {
        foreach (Sound sound in au_ListSounds)
        {
            if (sound.str_name == name)
            {
                sound.au_source.Play();
            }
        }
    }
}
