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
    size = transform.localScale;

    ChangeWaypoint();
  }

  public void FireSpawn()
  {
    int nbOfFire = Random.Range(nbMinOfFire, nbMaxOfFire);
    for (int i = 0; i < nbOfFire; ++i)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        bool grounded = Physics.Raycast(ray, out hit);
        if (grounded && hit.transform.tag.Equals("Ground"))
        {
            Debug.Log("touch");
            //FireSpawn();
            if (!chunckIsLoad) chunckIsLoad = true;
        }
        else if (chunckIsLoad)
        {
            chunckIsLoad = false;
            FireUnspawn();
        }
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
      Debug.Log("All the fire are desactived");
      // Generate random position inside a circle of radius 3000 and center at the current position while the altitude is not between min and max altitude
      Vector2 randomPos;
      float altitude;
      do
      {
        randomPos = Random.insideUnitCircle * 1000;
        altitude = GetAltitude(randomPos.x, randomPos.y);
      }
      while (altitude < minAltitude || altitude > maxAltitude);

      Vector3 newPos = new Vector3(randomPos.x, 1000, randomPos.y);
      transform.position = newPos;
      center = new Vector3(randomPos.x, altitude, randomPos.y);
      FireSpawn();
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

    public void FireUnspawn()
    {
        foreach (GameObject fire in fireList)
        {
            Destroy(fire);
        }
    }
}
