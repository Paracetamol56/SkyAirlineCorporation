using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField]
    private float totalSeconds;
    [SerializeField]
    private float maxIntensity;
    private Light lightComponent;
    private bool increasing = true;

    private void Start()
    {
        lightComponent = GetComponent<Light>();
        lightComponent.intensity = 0;
    }

    private void Update()
    {
        float waitTime = totalSeconds / 2;
        if (increasing)
            lightComponent.intensity += Time.deltaTime / waitTime;
        else
            lightComponent.intensity -= Time.deltaTime / waitTime;
        if (lightComponent.intensity >= maxIntensity)
            increasing = false;
        else if (lightComponent.intensity <= 0)
            increasing = true;
    }
}