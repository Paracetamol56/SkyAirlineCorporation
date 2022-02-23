using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDeliveryArea : MonoBehaviour
{
    [SerializeField]
    private float minAltitude = 0.0f;
    [SerializeField]
    private float maxAltitude = 0.0f;

    [SerializeField]
    private GameObject prefab;

    private GameObject spawnedObject;

    // starting corouting
    public IEnumerator Start()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Change waypoint
        ChangeWaypoint();

        // Generate Delivery Area
        DeliveryAreaSpawn();

        spawnedObject = GameObject.Find("SmokePS(Clone)");

        Timer.instance.StartTimer();
    }

    void Update()
    {
        if (SystemDeliveryArea.instance.GetState())
        {
            SystemDeliveryArea.instance.ChangeState();
            StartCoroutine(timeBeforeChangeWaypoint());
        }
    }

    public void DeliveryAreaSpawn()
    {
        Vector2 randomCircle = Random.insideUnitCircle * 10000;
        randomCircle += new Vector2(transform.position.x, transform.position.z);
        Vector3 pos = new Vector3(randomCircle.x, GetAltitude(randomCircle.x, randomCircle.y), randomCircle.y);
        Instantiate(prefab, pos, Quaternion.Euler(-90, 0, 0));
    }

    public void ChangeWaypoint()
    {
        // Generate random position inside a circle of radius 3000 and center at the current position while the altitude is not between min and max altitude

        Vector2 randomPos;
        float altitude;
        do
        {
            randomPos = Random.insideUnitCircle * 10000;
            altitude = GetAltitude(randomPos.x, randomPos.y);
        }
        while (altitude < minAltitude || altitude > maxAltitude);
        Debug.Log("randomPos : " + randomPos);

        if (spawnedObject != null)
        {
            Debug.Log("Lets go position change");
            spawnedObject.GetComponent<Transform>().position = new Vector3(randomPos.x, altitude, randomPos.y);
            transform.GetComponent<Transform>().position = new Vector3(randomPos.x, altitude, randomPos.y);
            Timer.instance.ResetTimer();
        }
        else Debug.LogWarning("No composant");
    }

    public float GetAltitude(float x, float z)
    {
        // Get the altitude of the terrain at the given position

        // Mask to get only the ground layer
        int mask = 1 << 3;

        // Raycast test
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 10000, z), Vector3.down, out hit, 50000, mask))
        {
            Debug.Log("Hit : " + hit.collider.name);
            Debug.DrawRay(new Vector3(x, 10000, z), Vector3.down * hit.distance, Color.red);
            float raycastDistance = hit.distance;
            raycastDistance = 10000 - raycastDistance;
            // Debug raycast result
            Debug.Log("Raycast hit at " + hit.point + " with height " + raycastDistance);
            return raycastDistance;
        }
        Debug.Log("Raycast missed");
        return 1000;
    }

    private IEnumerator timeBeforeChangeWaypoint()
    {
        yield return new WaitForSeconds(10.0f);
        ChangeWaypoint();
    }
}
