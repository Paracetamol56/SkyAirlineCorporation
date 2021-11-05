using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailsController : MonoBehaviour
{
    [SerializeField]
    private float minVelocity = 150.0f;

    private TrailRenderer trailRenderer;
    private Rigidbody rigidbody;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > minVelocity)
            trailRenderer.enabled = true;
        else
            trailRenderer.enabled = false;
    }
}
