using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeControl : MonoBehaviour
{

    [SerializeField] string volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] float sliderValue;
    [SerializeField] private float multiplier = 30f;
    // Start is called before the first frame update
    
    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }
    
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter,slider.value);
    }
    private void HandleSliderValueChanged(float value)
    {
        mixer.SetFloat(volumeParameter,Mathf.Log10(value)* multiplier);
    }
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, sliderValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
