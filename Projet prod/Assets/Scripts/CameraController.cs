using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Target to follow
    [SerializeField]
    Transform targetTransform;

    // Offset vector
    [SerializeField]
    Vector3 offsetPosition = new Vector3(0, 7, -18);

    // Rotation parameter
    [SerializeField]
    Vector3 offsetRotation = new Vector3(12, 0, 0);

    private void FixedUpdate()
    {
        Vector3 position = targetTransform.position + (targetTransform.rotation * offsetPosition);
        Quaternion rotation = targetTransform.rotation * Quaternion.Euler(offsetRotation);

        transform.rotation = rotation;
        transform.position = position;
    }
}
