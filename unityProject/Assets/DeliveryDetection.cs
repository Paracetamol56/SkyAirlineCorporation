using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryDetection : MonoBehaviour
{
    private GameObject spawnedArea;
    private bool alreadyPaid;

    private void Start()
    {
        StartCoroutine(timeBeforeAutoDelete());
        alreadyPaid = false;
        spawnedArea = GameObject.Find("SmokePS(Clone)");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !alreadyPaid)
        {
            alreadyPaid = true;
            calculateMoney();
        }
    }

    private void calculateMoney()
    {
        float distance = Vector3.Distance(spawnedArea.GetComponent<Transform>().position, transform.position);
        int parcelValue = 2000;

        if (distance <= 100)
        {
            float finalValue = Mathf.Clamp(distance, 0, 100) * parcelValue / 100;
            BalanceIndicator.instance.AddMoney(finalValue);
        }
    }

    private IEnumerator timeBeforeAutoDelete()
    {
        yield return new WaitForSeconds(10.0f);
        alreadyPaid = false;
        Destroy(transform.gameObject);
    }
}
