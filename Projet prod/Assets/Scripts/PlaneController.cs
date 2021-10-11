using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // Input axis
    private float throttle = 0.0f;
    private float yawAxis = 0.0f;
    private float pitchAxis = 0.0f;
    private float rollAxis = 0.0f;

    [SerializeField]
    private float maxSpeed = 200.0f;

    // RigidBody
    private Rigidbody planeRigidBody;

    private void Start()
    {
        planeRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        throttle += Input.GetAxis("Throttle");

        yawAxis = Input.GetAxis("Yaw");
        pitchAxis = Input.GetAxis("Pitch");
        rollAxis = Input.GetAxis("Roll");
    }

    private void FixedUpdate()
    {
        
    }
}
