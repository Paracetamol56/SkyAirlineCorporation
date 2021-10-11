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

    // Plane parameters
    [Header("Plane parameters")]
    [SerializeField]
    private float maxThrottle = 200.0f;
    [SerializeField]
    private float autoStabilization = 1.0f;
    [SerializeField]
    private float throttleInputMultiplicator = 1.0f;
    [SerializeField]
    private float inputMultiplicator = 3.0f;


    // RigidBody
    private Rigidbody planeRigidBody;

    private void Start()
    {
        planeRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Throttle input
        throttle = Mathf.Clamp(throttle + Input.GetAxis("Throttle") * throttleInputMultiplicator, 0.0f, maxThrottle);

        // Axis inputs
        yawAxis = Input.GetAxis("Yaw") * inputMultiplicator;
        pitchAxis = Input.GetAxis("Pitch") * inputMultiplicator;
        rollAxis = Input.GetAxis("Roll") * inputMultiplicator;
    }

    private void FixedUpdate()
    {
        // Rigid body forces and torques
        planeRigidBody.AddRelativeTorque(new Vector3(pitchAxis, yawAxis, rollAxis - yawAxis / 4.0f), ForceMode.Acceleration);
        planeRigidBody.AddRelativeForce(new Vector3(0.0f, 9.81f, throttle), ForceMode.Acceleration);

        // Auto stabilization
        Vector3 stabilizationTorque = Vector3.Cross(transform.up, Vector3.up);
        stabilizationTorque = Vector3.Project(stabilizationTorque, transform.forward);
        planeRigidBody.AddTorque(stabilizationTorque * autoStabilization, ForceMode.Acceleration);
    }
}
