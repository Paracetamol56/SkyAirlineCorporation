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

    private float speed = 0.0f;
    private float speedRef;
    private bool isGrounded = false;
    private PlaneObjectController planeObjectController;

    // RigidBody
    private Rigidbody planeRigidBody;

    // Debuging Canevas
    /*[Header("Debuging Canevas")]
    [SerializeField]
    private Slider YawSlider;
    [SerializeField]
    private Slider PitchSlider;
    [SerializeField]
    private Slider RollSlider;
    [SerializeField]
    private Slider ThrottleSlider;
    [SerializeField]
    private Text speedText;*/

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
        planeObjectController = GetComponent<PlaneObjectController>();
    }

    /// <summary>
    /// Rendering computations
    /// </summary>
    private void Update()
    {
        // Throttle input
        throttle = Mathf.Clamp(throttle + (Input.GetAxis("Throttle") * throttleInputMultiplicator), 0.0f, maxThrottle);
        planeObjectController.UpdateThrottle(throttle / maxThrottle);

        // Axis inputs
        if (isGrounded)
            yawAxis = Input.GetAxis("Yaw") * 2.0f;
        else
            yawAxis = Input.GetAxis("Yaw") * 50.0f / (speed + 1.0f);
        pitchAxis = Input.GetAxis("Pitch") * 2.0f;
        rollAxis = Input.GetAxis("Roll") * 5.0f;

        planeObjectController.UpdateAngles(new Vector3(Input.GetAxis("Pitch"), Input.GetAxis("Yaw"), Input.GetAxis("Roll")));

        //UpdateUi();
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2.0f, LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;

            // Rigid body forces and torques
            planeRigidBody.AddRelativeTorque(new Vector3(0, yawAxis * Mathf.Sqrt(speed), 0), ForceMode.Acceleration);
            planeRigidBody.AddRelativeForce(new Vector3(0.0f, lift, speed), ForceMode.Acceleration);
        }
        else
        {
            isGrounded = false;

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
    /// Update the debugging UI
    /// </summary>
    /*private void UpdateUi()
    {
        YawSlider.value = yawAxis / inputMultiplicator;
        PitchSlider.value = pitchAxis / inputMultiplicator;
        RollSlider.value = - rollAxis / inputMultiplicator;
        ThrottleSlider.value = throttle / maxThrottle;
        speedText.text = Vector3.Magnitude(planeRigidBody.velocity).ToString();
    }*/
}