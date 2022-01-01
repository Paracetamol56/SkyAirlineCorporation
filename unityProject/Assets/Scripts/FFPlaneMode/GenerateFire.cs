using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFire : MonoBehaviour
{
    public GameObject firePrefab;

    public Vector3 center;
    public Vector3 size;

    public int nbOfFire = 4;

    public void FireSpawn()
    {
        for (int i = 0; i < nbOfFire; ++i)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.z / 2, -size.z / 2));
            Instantiate(firePrefab, pos, Quaternion.identity);
        }
    }
}
