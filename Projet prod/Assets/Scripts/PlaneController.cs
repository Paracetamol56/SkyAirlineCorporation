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

    // Player parameters
    [SerializeField]
    private float maxSpeed = 200.0f;
    [SerializeField]
    private float autoCorrection = 1.0f;

    // RigidBody
    private Rigidbody planeRigidBody;

    private void Start()
    {
        planeRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (throttle <= maxSpeed)
        {
            throttle += Input.GetAxis("Throttle");
        }

        yawAxis = Input.GetAxis("Yaw");
        pitchAxis = Input.GetAxis("Pitch");
        rollAxis = Input.GetAxis("Roll");
    }

    private void FixedUpdate()
    {
        planeRigidBody.AddRelativeTorque(new Vector3(pitchAxis, yawAxis, rollAxis), ForceMode.Force);
        planeRigidBody.AddRelativeForce(new Vector3(0.0f, 9.81f, throttle), ForceMode.Force);

        // Auto correction
        planeRigidBody.AddRelativeTorque(new Vector3(autoCorrection * transform.rotation.x * transform.rotation.x, 0,
                                                     autoCorrection * transform.rotation.z * transform.rotation.z),ForceMode.Force);
    }
}
