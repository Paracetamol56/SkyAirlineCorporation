using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Tooltip("Minimum speed to keep the airplane in the air.\nWill be modified by angle of attack")]
    private float averrageMinFlightSpeed = 70.0f;
    [SerializeField]
    [Tooltip("Auto stabilization rotate the plane into standard horizontal position.\n0 = not at all\n1 = normal stabilization")]
    private float autoStabilization = 1.0f;
    [SerializeField]
    [Tooltip("Defines input sensitivity for throttle only.")]
    private float throttleInputMultiplicator = 1.0f;
    [SerializeField]
    [Tooltip("Defines input sensitivity.")]
    private float inputMultiplicator = 1.0f;
    [SerializeField]
    [Tooltip("This coefficient is used to compute the plane lift from the z velocity.\nIn perfect flight, lift should be equal to 9.81 * mass in order to compensate plane weight.\nUsually defined experimentally using air density, wings area, shape and inclination. Here we are using a simplified version, so take what works the best.")]
    private float liftCoefficient = 1000.0f;
    [SerializeField]
    [Tooltip("Offset on Y axis to know where to start the raycast used for ground detections")]
    private float groundDetectionOffset = 0.0f;

    private float speed = 0.0f;
    private float speedRef;
    private bool isGrounded = false;
    private ObjectController objectController;
    private Rigidbody planeRigidBody;

#if UNITY_EDITOR
    /// <summary>
    /// Inspector inputs verifications
    /// </summary>
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

    /// <summary>
    /// Called once if the object wakes up correctly
    /// </summary>
    private void Start()
    {
        planeRigidBody = GetComponent<Rigidbody>();
        objectController = GetComponent<ObjectController>();
    }

    /// <summary>
    /// Rendering computations
    /// </summary>
    private void Update()
    {
        // Throttle input
        throttle = Mathf.Clamp(throttle + (Input.GetAxis("Throttle") * throttleInputMultiplicator), 0.0f, maxThrottle);
        objectController.UpdateThrottle(throttle / maxThrottle);

        // Axis inputs
        if (isGrounded)
            yawAxis = Input.GetAxis("Yaw") * 2.0f * inputMultiplicator;
        else
            yawAxis = (Input.GetAxis("Yaw") * 50.0f * inputMultiplicator) / (speed + 1.0f);

        pitchAxis = Input.GetAxis("Pitch") * 2.0f * inputMultiplicator;
        rollAxis = Input.GetAxis("Roll") * 5.0f * inputMultiplicator;

        objectController.UpdateAngles(new Vector3(Mathf.Clamp(pitchAxis * 3, -1, 1), Mathf.Clamp(yawAxis * 3, -1, 1), Mathf.Clamp(rollAxis, -1, 1)));
    }

    /// <summary>
    /// Physic computations
    /// </summary>
    private void FixedUpdate()
    {
        // Speed calculation (independant of isGrounded)
        speed = Mathf.SmoothDamp(speed, throttle, ref speedRef, 10.0f);

        // Lift calculation
        float zVelocity = Vector3.Magnitude(new Vector3(0, 0, planeRigidBody.velocity.z));
        float lift = (zVelocity * zVelocity) / liftCoefficient;

        // Ground verification (independant of isGrounded)
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, groundDetectionOffset, 0), transform.TransformDirection(Vector3.down), out hit, 2.0f))
        {
            if (hit.collider.tag == "Ground")
                isGrounded = true;
            else
                isGrounded = false;
        }
        else
            isGrounded = false;

        if (isGrounded)
        {
            // Rigid body forces and torques
            planeRigidBody.AddRelativeTorque(new Vector3(pitchAxis, yawAxis * Mathf.Sqrt(speed), 0), ForceMode.Acceleration);
            planeRigidBody.AddRelativeForce(new Vector3(0.0f, lift * 1.5f, speed), ForceMode.Acceleration);
        }
        else
        {
            // Apply minFlightSpeed if plane is in the air
            float angleOfAttack = transform.localRotation.x * 180f;
            if (angleOfAttack <= 15)
                speed = Mathf.Max(averrageMinFlightSpeed + angleOfAttack, speed);

            // Rigid body forces and torques
            planeRigidBody.AddRelativeTorque(new Vector3(pitchAxis, yawAxis, rollAxis - yawAxis), ForceMode.Acceleration);
            planeRigidBody.AddRelativeForce(new Vector3(0.0f, lift, speed + (transform.localRotation.x * 50.0f)), ForceMode.Acceleration);

            // Auto stabilization
            Vector3 stabilizationTorque = Vector3.Cross(transform.up, Vector3.up);
            stabilizationTorque = Vector3.Project(stabilizationTorque, transform.forward);
            planeRigidBody.AddTorque(stabilizationTorque * autoStabilization, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Used by DashboardUI
    /// </summary>
    /// <returns>throttle : the acceleration input</returns>
    public float GetThrottle()
    {
        return throttle;
    }

    public float getSpeed()
    {
        return planeRigidBody.velocity.magnitude;
    }
}
