using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructionManager : MonoBehaviour
{
    public GameObject explosion;
    public bool drowned = false;
    public GameObject ExplosionSound;
    public GameObject SplashSound;
    public GameObject DeathLoader;
    private Transform PlanePos;
    private Rigidbody rigidBody;

    public Camera MainCamera;
    private CameraController CamScript;
    private PlaneController PlayerController;

    float LandingSpeedMax = 125.0f;

    private AudioSource audio;



    void Start()
    {
        PlanePos = this.GetComponent<Transform>();
        rigidBody = this.GetComponent<Rigidbody>();
        CamScript = MainCamera.GetComponent<CameraController>();
        PlayerController = gameObject.GetComponent<PlaneController>();
        audio = gameObject.GetComponent<AudioSource>();

    }

    void OnCollisionEnter(Collision col)
    {
        Vector3 planeNormal = PlanePos.up;
        Vector3 ContactSol = col.contacts[0].normal;
        float DotProduct = Vector3.Dot(planeNormal, ContactSol);
        if (col.gameObject.tag == "Ground")
        {
            if ((DotProduct < 0.90f && DotProduct > -0.90f) || (rigidBody.velocity.magnitude >= LandingSpeedMax))
            {
                int nbExplosion = Random.Range(3, 6);
                Instantiate(ExplosionSound, PlanePos.position, Quaternion.identity);
                Instantiate(DeathLoader, PlanePos.position, Quaternion.identity);
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

    void Update()
    {
        if (PlanePos.position.y < 310f)
        {
            if (drowned == false)
                SpawnDrownGameObject();
        }

        if (drowned)
        {
            PlayerController.SetThrottleAverageSpeed(0f, 0f);
            StartCoroutine(SoundDown());
        }

    }

    void SpawnDrownGameObject()
    {
        drowned = true;
        Instantiate(SplashSound, PlanePos.position, Quaternion.identity);
        Instantiate(DeathLoader, PlanePos.position, Quaternion.identity);
    }


    IEnumerator SoundDown()
    {
        yield return new WaitForSeconds(0.1f);
        audio.volume -= 0.01f;
    }


}
