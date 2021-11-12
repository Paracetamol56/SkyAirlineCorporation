using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltigeObjectController : ObjectController 
{
    // Yaw axis
    [SerializeField]
    private Transform rubber;

    // Pitch axis
    [SerializeField]
    private Transform elevators;

    // Roll axis
    [SerializeField]
    private Transform leftAilerons;
    [SerializeField]
    private Transform rightAilerons;

    // Propellers
    [SerializeField]
    private Transform propeller;

    /// <summary>
    /// Update rubber, elevators and ailerons angles
    /// </summary>
    /// <param name="angles">angles contains 3 floating number from input between -1 and 1</param>
    public override void UpdateAngles(Vector3 angles)
    {
        rubber.localRotation = Quaternion.Euler(-90, Mathf.Lerp(rubberAmplitude, -rubberAmplitude, (angles.y + 1) / 2), 0);

        elevators.localRotation = Quaternion.Euler(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2) - 90, 0, 0);

        leftAilerons.localRotation = Quaternion.Euler(Mathf.Lerp(-aileronsAmplitude, aileronsAmplitude, (angles.z + 1) / 2) - 90, 0, 0);
        rightAilerons.localRotation = Quaternion.Euler(Mathf.Lerp(aileronsAmplitude, -aileronsAmplitude, (angles.z + 1) / 2) - 90, 0, 0);
    }

    /// <summary>
    /// Update propeller rotation speed
    /// </summary>
    /// <param name="throttle">throttle is between 0 and 1</param>
    public override void UpdateThrottle(float throttle)
    {
        float rotationSpeed = Mathf.Lerp(minPropellerSpeed, maxPropellerSpeed, throttle);

        propeller.Rotate(Vector3.forward, rotationSpeed);
    }
}
