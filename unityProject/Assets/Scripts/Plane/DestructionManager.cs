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
    private PlaneController PlayerController;


    void Start()
    {
        PlanePos = this.GetComponent<Transform>();
        rigidBody = this.GetComponent<Rigidbody>();
        CamScript = camera.GetComponent<CameraController>();
        PlayerController = gameObject.GetComponent<PlaneController>();

    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground" && PlayerController.getSpeed() > 100.0f)
        {
            Instantiate(explosion, PlanePos.position, Quaternion.identity);
            rigidBody.velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            CamScript.DestroyCam(PlanePos);
            gameObject.SetActive(false);


        }
    }
}
