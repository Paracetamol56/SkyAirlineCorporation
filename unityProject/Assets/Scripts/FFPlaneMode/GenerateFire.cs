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
            Debug.Log("All the fire are desactived");
    }

    public void FireUnspawn()
    {
        foreach(GameObject fire in fireList)
        {
            Destroy(fire);
        }
    }
}
