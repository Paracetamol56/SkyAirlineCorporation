using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemDeliveryArea : MonoBehaviour
{
    private bool send;

    [SerializeField]
    private GameObject parcel;

    public static SystemDeliveryArea instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Erreur");
            return;
        }
        instance = new SystemDeliveryArea();
    }

    private void Start()
    {
        send = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !send)
        {
            Instantiate(parcel, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), new Quaternion(0, 0, 0, 0), null);
            send = true;
        }
    }

    public void ChangeState()
    {
        send = !send;
    }

    public bool GetState()
    {
        return send;
    }
}
