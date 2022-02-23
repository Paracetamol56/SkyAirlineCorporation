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
        float distance = Vector3.Distance(spawnedArea.GetComponent<Transform>().position, GetComponent<Transform>().position);
        int parcelValue = 2000;
        Debug.Log("Distance = " + distance.ToString());

        if (distance <= 100)
        {
            float finalValue = Mathf.Clamp(0, 100, distance) * parcelValue / 100;
            Debug.Log("Added Value = " + finalValue.ToString());
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
