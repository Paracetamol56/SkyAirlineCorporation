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

    // angles contains 3 floating number from input between -1 and 1
    public void UpdateAngles(Vector3 angles)
    {
        rubber.localRotation = Quaternion.Euler(-90, Mathf.Lerp(rubberAmplitude, -rubberAmplitude, (angles.y + 1) / 2), 0);

        leftElevator.localRotation = Quaternion.Euler(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2) - 90, 0, 0);
        rightElevator.localRotation = Quaternion.Euler(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2) - 90, 0, 0);

        leftAilerons.localRotation = Quaternion.Euler(Mathf.Lerp(-aileronsAmplitude, aileronsAmplitude, (angles.z + 1) / 2) - 90, 0, 0);
        rightAilerons.localRotation = Quaternion.Euler(Mathf.Lerp(aileronsAmplitude, -aileronsAmplitude, (angles.z + 1) / 2) - 90, 0, 0);
    }
}
