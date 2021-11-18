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

    // Smoothing effect
    [SerializeField]
    private float smoothness;

    private Vector3 cameraVelocity;
    private Vector3 orbitalRotation = new Vector3(0, 0, 0);

    private void Update()
    {
        orbitalRotation.y += Input.GetAxis("CameraYaw");
        orbitalRotation.x += Input.GetAxis("CameraPitch");
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            orbitalRotation = new Vector3(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        Vector3 position = Vector3.SmoothDamp(transform.position, targetTransform.position + ((targetTransform.rotation * Quaternion.Euler(orbitalRotation)) * offsetPosition), ref cameraVelocity, smoothness);
        // Quaternion rotation = targetTransform.rotation * Quaternion.Euler(offsetRotation);
        transform.LookAt(targetTransform, targetTransform.up);

        transform.position = position;
        //transform.rotation = rotation;
    }

    public void DestroyCam(Transform pos)
    {
        targetTransform = pos;
        smoothness = 0.5f;
        offsetPosition= new Vector3(0,50,-100);
    }
}
