using System.Collections;
using UnityEngine;

public class DestructionHandler : MonoBehaviour
{
    public GameObject explosion;
    public bool drowned = false;
    public GameObject ExplosionSound;
    public GameObject SplashSound;
    private Transform PlanePos;
    private Rigidbody rigidBody;
    private ManagerScene managerScene;

    public Camera MainCamera;
    private CameraController CamScript;
    private PlaneController PlayerController;

    float LandingSpeedMax = 125.0f;

    private AudioSource audioSource;
    private bool canExplode = true;

    void Start()
    {
        PlanePos = this.GetComponent<Transform>();
        rigidBody = this.GetComponent<Rigidbody>();
        CamScript = MainCamera.GetComponent<CameraController>();
        PlayerController = gameObject.GetComponent<PlaneController>();
        audioSource = gameObject.GetComponent<AudioSource>();
        managerScene = GameObject.Find("SceneManager").GetComponent<ManagerScene>();
    }

    void OnCollisionEnter(Collision col)
    {

        Vector3 planeNormal = PlanePos.up;
        Vector3 ContactSol = col.contacts[0].normal;
        float DotProduct = Vector3.Dot(planeNormal, ContactSol);
        if ((col.gameObject.tag == "Ground"|| col.gameObject.tag == "Gate") && canExplode == true)
        {
            canExplode = false;
            if ((DotProduct < 0.90f && DotProduct > -0.90f) || (rigidBody.velocity.magnitude >= LandingSpeedMax))
            {
                int nbExplosion = Random.Range(3, 6);
                Instantiate(ExplosionSound, PlanePos.position, Quaternion.identity);
                managerScene.setlastSceneIndex(managerScene.getCurrentSceneIndex());
                managerScene.setCurrentSceneIndex(SceneIndex.GameOver);
                managerScene.LoadGame();
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
            // TODO : Freeze the plane's controls
            //rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(SoundDown());
        }

    }


    void SpawnDrownGameObject()
    {
        drowned = true;
        Instantiate(SplashSound, PlanePos.position, Quaternion.identity);
        managerScene.setlastSceneIndex(managerScene.getCurrentSceneIndex());
        managerScene.setCurrentSceneIndex(SceneIndex.GameOver);
        managerScene.LoadGame();
    }

    IEnumerator SoundDown()
    {
        yield return new WaitForSeconds(0.1f);
        audioSource.volume -= 0.01f;
    }

}
