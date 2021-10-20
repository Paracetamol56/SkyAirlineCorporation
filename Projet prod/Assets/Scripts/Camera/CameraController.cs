using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Plane;
    public GameObject Camera;

    void Update()
    {
    Vector3 moveCamto = Plane.transform.position - Plane.transform.forward * 10.0f + PlaneController.transform.up * 5.0f;
    Camera.main.transform.position = moveCameTo;
    }
}
