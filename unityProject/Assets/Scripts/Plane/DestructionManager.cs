using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionManager : MonoBehaviour
{
    public GameObject explosion;
    public bool drowned = false;
    public GameObject ExplosionSound;
    private Transform PlanePos;
    private Rigidbody rigidBody;

    public Camera MainCamera;
    private CameraController CamScript;
    private PlaneController PlayerController;

    float LandingSpeedMax = 125.0f;


    void Start()
    {
        PlanePos = this.GetComponent<Transform>();
        rigidBody = this.GetComponent<Rigidbody>();
        CamScript = MainCamera.GetComponent<CameraController>();
        PlayerController = gameObject.GetComponent<PlaneController>();

    }

    void OnCollisionEnter(Collision col)
    {
        Vector3 planeNormal = PlanePos.up;
        Vector3 ContactSol = col.contacts[0].normal;
        float DotProduct = Vector3.Dot(planeNormal, ContactSol);
        Debug.Log("Vitesse = " + rigidBody.velocity.magnitude);
        if (col.gameObject.tag == "Ground")
        {
            if ((DotProduct < 0.90f && DotProduct > -0.90f) || (rigidBody.velocity.magnitude >= LandingSpeedMax))
            {
                Debug.Log("Angle = " + DotProduct);
                int nbExplosion = Random.Range(3, 6);
                Instantiate(ExplosionSound, PlanePos.position, Quaternion.identity);
                for (int i = 0; i < nbExplosion; ++i)
                {
                    Instantiate(explosion, PlanePos.position + (Random.insideUnitSphere * 15), Quaternion.identity);
                }
                rigidBody.velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                CamScript.DestroyCam(PlanePos);
                gameObject.SetActive(false);
            }

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Water")
        {
            Debug.Log("Water Damage");
            drowned = true;
            while (PlayerController.GetThrottle() != 0)
            {
                PlayerController.SetThrottleAverageSpeed(PlayerController.GetThrottle() - Time.deltaTime, 0f);
                if (PlayerController.GetThrottle() < 0)
                {
                    PlayerController.SetThrottleAverageSpeed(0f, 0f);
                }
            }
        }
    }

    void Update()
    {
        if (drowned)
        {
            PlayerController.SetThrottleAverageSpeed(0f, 0f);
        }
    }

}
