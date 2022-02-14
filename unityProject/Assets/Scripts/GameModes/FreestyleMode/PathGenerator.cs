using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathGenerator : MonoBehaviour
{
    [SerializeField]
    private float maxNextGenerationAngle = 30.0f;
    [SerializeField]
    private AnimationCurve distributionNextGenerationAngle;
    [SerializeField]
    private float maxNextGenerationDistance = 100.0f;
    [SerializeField]
    private AnimationCurve distributionNextGenerationDistance;
    [SerializeField]
    private int gateSpacing = 50;

    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private GameObject gatePrefab;

    private List<GameObject> gateCircularBuffer = new List<GameObject>();
    [SerializeField]
    private uint gateCircularBufferSize = 4;

    void Start()
    {
        // Srtating path
        for (int i = 0; i < gateCircularBufferSize; i++)
        {
            StartCoroutine(GenerateNextGate());
        }
    }

    private IEnumerator GenerateNextGate()
    {
        for (int i = 0; i < gateSpacing; i++)
        {
            goToNextPoint();
            Instantiate(pointPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.01f);
        }
        // Instanciate a new gate
        GameObject newGate = Instantiate(gatePrefab, transform.position, transform.rotation);
        gateCircularBuffer.Add(newGate);
        if (gateCircularBuffer.Count > 10)
        {
            Destroy(gateCircularBuffer[0]);
            gateCircularBuffer.RemoveAt(0);
        }
    }

    private void goToNextPoint()
    {
        float angleHorizontal = Random.Range(-1.0f, 1.0f);
        float angleVertical = Random.Range(-1.0f, 1.0f);
        angleHorizontal = distributionNextGenerationAngle.Evaluate(angleHorizontal) * maxNextGenerationAngle;
        angleVertical = distributionNextGenerationAngle.Evaluate(angleVertical) * maxNextGenerationAngle;

        float distance = Random.Range(0.0f, 1.0f);
        distance = distributionNextGenerationDistance.Evaluate(distance) * maxNextGenerationDistance;

        Debug.Log("angleHorizontal: " + angleHorizontal);
        Debug.Log("angleVertical: " + angleVertical);
        Debug.Log("distance: " + distance);
        transform.Rotate(angleHorizontal, 0.0f, angleVertical);
        transform.Translate(0.0f, 0.0f, distance);
    }
}
