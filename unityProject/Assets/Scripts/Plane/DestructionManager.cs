using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionManager : MonoBehaviour
{
    public GameObject explosion;
    private Transform PlanePos;
    private Rigidbody rigidBody;

    public Camera camera;
    private CameraController CamScript;


    void Start()
    {
        PlanePos = this.GetComponent<Transform>();
        rigidBody = this.GetComponent<Rigidbody>();
        CamScript = camera.GetComponent<CameraController>();

    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Instantiate(explosion, PlanePos.position, Quaternion.identity);
            rigidBody.velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            CamScript.DestroyCam(PlanePos);

        }
    }
}
