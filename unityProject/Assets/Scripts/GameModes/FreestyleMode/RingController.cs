using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    List<GameObject> propellers = new List<GameObject>();

    private void Start()
    {
        GameObject support = transform.Find("Support").gameObject;
        support.transform.rotation = Quaternion.Euler(0, 0, 0);

        // Get all gameobjects children of the support
        for (int i = 0; i < support.transform.childCount; i++)
        {
            propellers.Add(support.transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        // Rotate the propellers
        for (int i = 0; i < propellers.Count; i++)
        {
            propellers[i].transform.Rotate(0, Time.deltaTime, 0);
        }
    }
}
