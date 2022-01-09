using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    public AudioSource music;
    public bool muted = false;

    public void Mute()
    {
        if(muted)
        {
            muted = false;
        }
        else
        {
            muted = true;
        }
    }

    void Update()
    {
        if(muted)
        {
            music.volume = 0f;
        }
        else
        {
            music.volume = 0.1f;
        }
    }
}
