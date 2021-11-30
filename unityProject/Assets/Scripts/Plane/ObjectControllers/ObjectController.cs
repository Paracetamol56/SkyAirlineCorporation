using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectController : MonoBehaviour
{
    // Yaw axis
    [SerializeField]
    protected float rubberAmplitude = 10.0f;

    // Pitch axis
    [SerializeField]
    protected float elevatorAmplitude = 10.0f;

    // Roll axis
    [SerializeField]
    protected float aileronsAmplitude = 10.0f;

    // Propellers
    [SerializeField]
    protected float minPropellerSpeed = 20.0f;
    [SerializeField]
    protected float maxPropellerSpeed = 100.0f;

    // Fuel
    [SerializeField]
    protected Slider slider;
    [SerializeField]
    protected Gradient gradient;
    [SerializeField]
    protected Image fill;
    protected float fuel;

    /// <summary>
    /// Update rubber, elevators and ailerons angles
    /// </summary>
    /// <param name="angles">angles contains 3 floating number from input between -1 and 1</param>
    public virtual void UpdateAngles(Vector3 angles) { }

    /// <summary>
    /// Update propeller rotation speed
    /// </summary>
    /// <param name="throttle">throttle is between 0 and 1</param>
    public virtual void UpdateThrottle(float throttle) { }

    public float setFuel
    {
        get { return fuel; }
        set
        {
            if (value < 0)
                fuel = 0;
            else
                fuel = value;
        }
    }
}
