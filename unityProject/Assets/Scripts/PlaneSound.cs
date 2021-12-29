using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSound : MonoBehaviour
{
    private AudioSource audioSource;
    private PlaneController Controller;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        Controller = GetComponent<PlaneController>();
    }

    void Update(){
          UpdateSound();
    }   

    void UpdateSound(){
          audioSource.volume = Mathf.InverseLerp(0f, Controller.getMaxSpeed()*2, Controller.getSpeed());
    }
}
