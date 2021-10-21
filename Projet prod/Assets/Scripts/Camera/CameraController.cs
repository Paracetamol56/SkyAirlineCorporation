using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Target to follow
    [SerializeField]
    private Transform targetTransform;

    // Offset vector
    [SerializeField]
    private Vector3 offsetPosition = new Vector3(0, 7, -18);

    // Rotation parameter
    [SerializeField]
    private Vector3 offsetRotation = new Vector3(12, 0, 0);

    // Smoothing effect
    [SerializeField]
    private float smoothness;

    private Vector3 cameraVelocity;

    private void FixedUpdate()
    {
        Vector3 position = Vector3.SmoothDamp(transform.position, targetTransform.position + (targetTransform.rotation * offsetPosition), ref cameraVelocity, smoothness);
        Quaternion rotation = targetTransform.rotation * Quaternion.Euler(offsetRotation);

        transform.position = position;
        transform.rotation = rotation;
    }
}
