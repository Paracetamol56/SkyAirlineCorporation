using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFire : MonoBehaviour
{
    [SerializeField]
    private float minAltitude = 0.0f;
    [SerializeField]
    private float maxAltitude = 0.0f;

    [SerializeField]
    private GameObject firePrefab;

    private bool chunckIsLoad = false;

    private List<GameObject> fireList = new List<GameObject>();

    // starting corouting
    public IEnumerator Start()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Change waypoint
        ChangeWaypoint();
    }

    public void FireSpawn()
    {
        //                  \/ Here is fire count
        for (int i = 0; i < 50; ++i)
        {
            // Generate random position inside a circle of radius 25
            Vector2 randomCircle = Random.insideUnitCircle * 20;
            randomCircle += new Vector2(transform.position.x, transform.position.z);
            Vector3 pos = new Vector3(randomCircle.x, GetAltitude(randomCircle.x, randomCircle.y), randomCircle.y);
            GameObject fireCreated = Instantiate(firePrefab, pos, Quaternion.identity);
            fireCreated.transform.parent = transform;
            fireList.Add(fireCreated);
        }
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

        transform.position = new Vector3(randomPos.x, 1000, randomPos.y);

        FireSpawn();
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

    public void FireUnspawn()
    {
        foreach (GameObject fire in fireList)
        {
            Destroy(fire);
        }
    }

    public int ListLenght()
    {
        return fireList.Count;
    }
}
