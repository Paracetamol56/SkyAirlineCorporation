using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailsController : MonoBehaviour
{
    [SerializeField]
    private float minVelocity = 150.0f;

    [SerializeField]
    private List<TrailRenderer> trailRenderers;

    private Rigidbody rigidbody;
    private bool isActive = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        foreach (TrailRenderer elem in trailRenderers)
            elem.enabled = false;
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > minVelocity && !isActive)
        {
            isActive = true;
            foreach (TrailRenderer elem in trailRenderers)
                elem.enabled = true;
        }
        else if (rigidbody.velocity.magnitude < minVelocity && isActive)
        {
            isActive = false;
            foreach (TrailRenderer elem in trailRenderers)
                elem.enabled = false;
        }
    }
}
