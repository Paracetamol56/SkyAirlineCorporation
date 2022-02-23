using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryDetection : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(timeBeforeAutoDelete());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("DeliveryArea"))
        {
            Debug.Log("colis recu dans la zone de livraison");
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("colis nest pas recu dans la zone de livraison");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("colis nest pas recu dans la zone de livraison");
        }
    }

    private IEnumerator timeBeforeAutoDelete()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(transform.gameObject);
    }
}
