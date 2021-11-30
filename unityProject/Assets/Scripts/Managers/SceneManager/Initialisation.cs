using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Initialisation : MonoBehaviour
{
    public static Initialisation current;

    public GameObject[] pedestrianPrefabs;
    public int pedestriansToSpawn;

    public float progress;
    public bool isDone;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     StartCoroutine(Spawn());
    // }

    // IEnumerator Spawn()
    // {
    //     int count = 0;
    //     while (count < pedestriansToSpawn)
    //     {

    //     }
    // }
}
