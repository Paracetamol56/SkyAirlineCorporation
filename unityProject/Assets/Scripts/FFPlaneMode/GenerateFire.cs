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

    private Vector3 center;
    private Vector3 size;

    private int nbMinOfFire = 4;
    private int nbMaxOfFire = 8;
    private bool chunckIsLoad = false;

    private List<GameObject> fireList = new List<GameObject>();

    void Start()
    {
        center = transform.position;
        size = transform.localScale * 10;

        ChangeWaypoint();
    }

    public void FireSpawn()
    {
        int nbOfFire = Random.Range(nbMinOfFire, nbMaxOfFire);
        for (int i = 0; i < nbOfFire; ++i)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.z / 2, -size.z / 2));
            GameObject fireCreated = Instantiate(firePrefab, pos, Quaternion.identity);
            fireList.Add(fireCreated);
        }
    }

    public void ChangeWaypoint()
    {
        randomPos = Random.insideUnitCircle * 5000;
        altitude = GetAltitude(randomPos.x, randomPos.y);
    }

    public float GetAltitude(float x, float z)
    {
        // Raycast test
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 10000, z), Vector3.down, out hit, 50000))
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
        return 0;
    }

    public void FireUnspawn()
    {
        foreach (GameObject fire in fireList)
        {
            Destroy(fire);
        }
    }
}

