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
    private Transform leftAileron;
    [SerializeField]
    private Transform rightAileron;
    private Quaternion initialLeftAileron;
    private Quaternion initialRightAileron;

    // Propellers
    [SerializeField]
    private Transform propeller;

    private void Start()
    {
        initialLeftAileron = leftAileron.rotation;
        initialRightAileron = rightAileron.rotation;
    }

    /// <summary>
    /// Update rubber, elevators and ailerons angles
    /// </summary>
    /// <param name="angles">angles contains 3 floating number from input between -1 and 1</param>
    public override void UpdateAngles(Vector3 angles)
    {
        rubber.localRotation = Quaternion.Euler(-90, Mathf.Lerp(rubberAmplitude, -rubberAmplitude, (angles.y + 1) / 2), 0);

        elevators.localRotation = Quaternion.AngleAxis(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2), Vector3.back) * Quaternion.AngleAxis(90, Vector3.right);

        leftAileron.localRotation = Quaternion.Euler(90, 188.5f, 0) * Quaternion.AngleAxis(Mathf.Lerp(-aileronsAmplitude, aileronsAmplitude, (angles.z + 1) / 2), Vector3.down);
        rightAileron.localRotation = Quaternion.Euler(-90, -8.5f, 0) * Quaternion.AngleAxis(Mathf.Lerp(aileronsAmplitude, -aileronsAmplitude, (angles.z + 1) / 2), Vector3.down);
    }

    /// <summary>
    /// Update propeller rotation speed
    /// </summary>
    /// <param name="throttle">throttle is between 0 and 1</param>
    public override void UpdateThrottle(float throttle)
    {
        float rotationSpeed = Mathf.Lerp(minPropellerSpeed, maxPropellerSpeed, throttle);

        propeller.Rotate(Vector3.right, rotationSpeed);
    }
}
