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
    [Tooltip("Maximum amount of throttles you are allowed to use.")]
    private float maxThrottle = 200.0f;
    [SerializeField]
    [Tooltip("Auto stabilization rotate the plane into standard horizontal position.\n0 = not at all\n1 = normal stabilization")]
    private float autoStabilization = 1.0f;
    [SerializeField]
    [Tooltip("Defines input sensitivity for throttle only.")]
    private float throttleInputMultiplicator = 1.0f;
    [SerializeField]
    [Tooltip("Defines input sensitivity.")]
    private float inputMultiplicator = 3.0f;
    [SerializeField]
    [Tooltip("This coefficient is used to compute the plane lift from the velocity.\nIn perfect flight, lift should be equal to 9.81 * mass in order to compensate plane weight.\nUsually defined experimentally using air density, wings area, shape and inclination. Here we are using a simplified version, so take what works the best.")]
    private float liftCoefficient = 130.0f;

    // RigidBody
    private Rigidbody planeRigidBody;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (maxThrottle <= 0.0f)
            maxThrottle = 1.0f;

        if (autoStabilization <= 0.0f)
            autoStabilization = 1.0f;

        if (throttleInputMultiplicator <= 0.0f)
            throttleInputMultiplicator = 1.0f;

        if (inputMultiplicator <= 0.0f)
            inputMultiplicator = 1.0f;

        if (liftCoefficient <= 0.0f)
            liftCoefficient = 0.01f;
    }
#endif

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
        // Lift calculation
        float velocity = Vector3.Magnitude(planeRigidBody.velocity);
        float lift = ((velocity * velocity) / liftCoefficient);

        // Rigid body forces and torques
        planeRigidBody.AddRelativeTorque(new Vector3(pitchAxis, yawAxis, rollAxis - yawAxis / 2.0f), ForceMode.Acceleration);
        planeRigidBody.AddRelativeForce(new Vector3(0.0f, lift, throttle), ForceMode.Acceleration);

        // Auto stabilization
        Vector3 stabilizationTorque = Vector3.Cross(transform.up, Vector3.up);
        stabilizationTorque = Vector3.Project(stabilizationTorque, transform.forward);
        planeRigidBody.AddTorque(stabilizationTorque * autoStabilization, ForceMode.Acceleration);
    }

}
