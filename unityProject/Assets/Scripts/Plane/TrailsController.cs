using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailsController : MonoBehaviour
{
    [SerializeField]
    private float minVelocity = 150.0f;

    [SerializeField]
    private List<TrailRenderer> trailRenderers;

    private Rigidbody planeRigidbody;
    private bool isActive = false;

    private void Start()
    {
        planeRigidbody = GetComponent<Rigidbody>();
        foreach (TrailRenderer elem in trailRenderers)
            elem.enabled = false;
    }

    private void FixedUpdate()
    {
        if (planeRigidbody.velocity.magnitude > minVelocity && !isActive)
        {
            isActive = true;
            foreach (TrailRenderer elem in trailRenderers)
                elem.enabled = true;
        }
        else if (planeRigidbody.velocity.magnitude < minVelocity && isActive)
        {
            isActive = false;
            foreach (TrailRenderer elem in trailRenderers)
                elem.enabled = false;
        }
    }
}
