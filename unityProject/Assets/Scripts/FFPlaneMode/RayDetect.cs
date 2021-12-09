using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDetect : MonoBehaviour
{
    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;

    public Vector3 wayPoint;

    private RayDetect instance;

    private RayDetect()
    {
        if(instance != null)
        {
            Debug.LogError("RayDetect existe deja");
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(wayPoint, Vector3.down, out hit, 1000))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }

    public void setWaypoint(GameObject ob)
    {
        wayPoint = ob.transform.position;
    }
}
