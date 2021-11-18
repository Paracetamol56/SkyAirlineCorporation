using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivraisonObjectController : ObjectController
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

    // Propellers
    [SerializeField]
    private Transform propeller;

    /// <summary>
    /// Update rubber, elevators and ailerons angles
    /// </summary>
    /// <param name="angles">angles contains 3 floating number from input between -1 and 1</param>
    public override void UpdateAngles(Vector3 angles)
    {
        rubber.localRotation = Quaternion.Euler(-90, 0, 0) * Quaternion.AngleAxis(31, Vector3.up) * Quaternion.AngleAxis(Mathf.Lerp(rubberAmplitude, -rubberAmplitude, (angles.y + 1) / 2), Vector3.forward);

        elevators.localRotation = Quaternion.AngleAxis(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2), Vector3.back) * Quaternion.AngleAxis(90, Vector3.right);

        leftAileron.localRotation = Quaternion.Euler(-90, 3.8f, 0) * Quaternion.AngleAxis(Mathf.Lerp(-aileronsAmplitude, aileronsAmplitude, (angles.z + 1) / 2), Vector3.down);
        rightAileron.localRotation = Quaternion.Euler(-90, -3.8f, 0) * Quaternion.AngleAxis(Mathf.Lerp(aileronsAmplitude, -aileronsAmplitude, (angles.z + 1) / 2), Vector3.down);
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
