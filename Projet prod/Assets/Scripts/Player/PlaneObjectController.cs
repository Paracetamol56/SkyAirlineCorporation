using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneObjectController : MonoBehaviour
{
    // Yaw axis
    [SerializeField]
    private Transform rubber;
    [SerializeField]
    private float rubberAmplitude = 10.0f;

    // Pitch axis
    [SerializeField]
    private Transform leftElevator;
    [SerializeField]
    private Transform rightElevator;
    [SerializeField]
    private float elevatorAmplitude = 10.0f;

    // Roll axis
    [SerializeField]
    private Transform leftAilerons;
    [SerializeField]
    private Transform rightAilerons;
    [SerializeField]
    private float aileronsAmplitude = 10.0f;

    // Propellers
    [SerializeField]
    private Transform leftPropeller;
    [SerializeField]
    private Transform rightPropeller;
    [SerializeField]
    private float minPropellerSpeed = 0.0f;
    [SerializeField]
    private float maxPropellerSpeed = 0.0f;


    /// <summary>
    /// Update rubber, elevators and ailerons angles
    /// </summary>
    /// <param name="angles">angles contains 3 floating number from input between -1 and 1</param>
    public void UpdateAngles(Vector3 angles)
    {
        rubber.localRotation = Quaternion.Euler(-90, Mathf.Lerp(rubberAmplitude, -rubberAmplitude, (angles.y + 1) / 2), 0);

        leftElevator.localRotation = Quaternion.Euler(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2) - 90, 0, 0);
        rightElevator.localRotation = Quaternion.Euler(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2) - 90, 0, 0);

        leftAilerons.localRotation = Quaternion.Euler(Mathf.Lerp(-aileronsAmplitude, aileronsAmplitude, (angles.z + 1) / 2) - 90, 0, 0);
        rightAilerons.localRotation = Quaternion.Euler(Mathf.Lerp(aileronsAmplitude, -aileronsAmplitude, (angles.z + 1) / 2) - 90, 0, 0);
    }

    /// <summary>
    /// Update propeller rotation speed
    /// </summary>
    /// <param name="throttle">throttle is between 0 and 1</param>
    public void UpdateThrottle(float throttle)
    {
        float rotationSpeed = Mathf.Lerp(minPropellerSpeed, maxPropellerSpeed, throttle);

        leftPropeller.Rotate(leftPropeller.forward, rotationSpeed);
        rightPropeller.Rotate(rightPropeller.forward, rotationSpeed);
    }
}
