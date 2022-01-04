using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSound : MonoBehaviour
{
    private AudioSource audioSource;
    private PlaneController Controller;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Controller = GetComponent<PlaneController>();
    }

    void Update()
    {
        UpdateSound();
    }

    void UpdateSound()
    {
        audioSource.pitch = Mathf.InverseLerp(0.0f, Controller.getMaxThrottle(), Controller.GetThrottle()) * 0.6f + 0.5f;

    }

}
