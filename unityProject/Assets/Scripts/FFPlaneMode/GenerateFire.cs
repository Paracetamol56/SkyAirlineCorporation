using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFire : MonoBehaviour
{
  private GameObject firePrefab;

  private Vector3 center;
  private Vector3 size;

  private int nbMinOfFire = 4;
  private int nbMaxOfFire = 8;
  private bool chunckIsLoad = false;

  private List<GameObject> fireList = new List<GameObject>();

  void Start()
  {
    firePrefab = Resources.Load("Fire") as GameObject;
    center = transform.position;
    size = transform.localScale;

    ChangeWaypoint();
  }

  private void FixedUpdate()
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
    int nbOfFireDisabled = 0;
    foreach (GameObject fire in fireList)
    {
      if (!fire.activeSelf)
        nbOfFireDisabled++;
    }

    if (nbOfFireDisabled == fireList.Count)
    {
      Debug.Log("All the fire are desactived");
      // Generate random position inside a circle of radius 3000 and center at the current position^
      Vector2 randomPos = Random.insideUnitCircle * 1000;
      GetAltitude(randomPos.x, randomPos.y);
      Vector3 newPos = new Vector3(randomPos.x, 1000, randomPos.y);
      transform.position = newPos;
    }
  }

  public float GetAltitude(float x, float z)
  {
    // Raycast test
    RaycastHit hit;
    if (Physics.Raycast(new Vector3(x, 10000, z), Vector3.down, out hit, 50000, LayerMask.GetMask("Ground")))
    {
      return hit.point.y;
    }
    else
    {
      return 0;
    }
    {
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
