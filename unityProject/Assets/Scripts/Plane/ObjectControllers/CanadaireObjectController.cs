using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanadaireObjectController : ObjectController
{
    // Yaw axis
    [SerializeField]
    private Transform rubber;

    // Pitch axis
    [SerializeField]
    private Transform leftElevator;
    [SerializeField]
    private Transform rightElevator;

    // Roll axis
    [SerializeField]
    private Transform leftAilerons;
    [SerializeField]
    private Transform rightAilerons;

    // Propellers
    [SerializeField]
    private Transform leftPropeller;
    [SerializeField]
    private Transform rightPropeller;

    // Landing gears attributes
    private bool landingGearsOut = true;
    private Animator animator;

    // Water Level
    private float water = 100f;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("landingGearsOut", landingGearsOut);
    }

    private void Update()
    {
        // Landing gears input
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (landingGearsOut)
                landingGearsOut = false;
            else
                landingGearsOut = true;
            animator.SetBool("landingGearsOut", landingGearsOut);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 8.0f))
        {
            if (hit.transform.tag == "Water")
            {
                water += 0.5f;
            }
            if(water >100f)
            {
                water = 100f;
            }
        }

    }

    /// <summary>
    /// Update rubber, elevators and ailerons angles
    /// </summary>
    /// <param name="angles">angles contains 3 floating number from input between -1 and 1</param>

    public override void UpdateAngles(Vector3 angles)
    {
        rubber.localRotation = Quaternion.Euler(-90, Mathf.Lerp(rubberAmplitude, -rubberAmplitude, (angles.y + 1) / 2), 0);

        leftElevator.localRotation = Quaternion.Euler(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2) - 90, 0, 0);
        rightElevator.localRotation = Quaternion.Euler(Mathf.Lerp(-elevatorAmplitude, elevatorAmplitude, (angles.x + 1) / 2) - 90, 0, 0);

        leftAilerons.localRotation = Quaternion.Euler(Mathf.Lerp(-aileronsAmplitude, aileronsAmplitude, (angles.z + 1) / 2) - 90, 0, 0);
        rightAilerons.localRotation = Quaternion.Euler(Mathf.Lerp(aileronsAmplitude, -aileronsAmplitude, (angles.z + 1) / 2) - 90, 0, 0);
    }

    /// <summary>
    /// Update propeller rotation speed
    /// </summary>
    /// <param name="throttle">throttle is between 0 and 1</param>
    public override void UpdateThrottle(float throttle)
    {
        float rotationSpeed = Mathf.Lerp(minPropellerSpeed, maxPropellerSpeed, throttle);

        leftPropeller.Rotate(Vector3.forward, rotationSpeed);
        rightPropeller.Rotate(Vector3.forward, rotationSpeed);
    }

    public float setWater
    {
        get { return water; }
        set
        {
            if (value < 0)
                water = 0;
            else
                water = value;
        }
    }

    public float getWater()
    {
        return water;
    }

    public void SetWater(float lvl)
    {
        water = lvl;
    }
}
