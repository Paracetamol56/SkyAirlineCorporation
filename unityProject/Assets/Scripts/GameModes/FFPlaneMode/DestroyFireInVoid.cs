using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFireInVoid : MonoBehaviour
{
    float xMin = -10f;
    float xMax = 10f;
    void Update()
    {
        if (transform.position.x > xMax || transform.position.x < xMin)
        {
            Destroy(this);
        }
    }
}
