using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathGenerator : MonoBehaviour
{
    [SerializeField]
    private float maxNextGenerationAngle = 30.0f;
    [SerializeField]
    private float nextGenerationDistance = 200.0f;
    [SerializeField]
    private int gateSpacing = 50;

    [SerializeField]
    private float maxAltitude = 2000.0f;
    [SerializeField]
    private float minAltitude = 500.0f;

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
            GenerateNextGate();
        }
    }

    public void GenerateNextGate()
    {
        for (int i = 0; i < gateSpacing; i++)
        {
            goToNextPoint();
            Instantiate(pointPrefab, transform.position, transform.rotation);
        }
        // Instanciate a new gate
        GameObject newGate = Instantiate(gatePrefab, transform.position, Quaternion.identity);
        newGate.transform.rotation = Quaternion.LookRotation(transform.position - newGate.transform.position);
        gateCircularBuffer.Add(newGate);
        if (gateCircularBuffer.Count > gateCircularBufferSize)
        {
            Destroy(gateCircularBuffer[0]);
            gateCircularBuffer.RemoveAt(0);
        }
    }

    private void goToNextPoint()
    {
        float noiseScale = 5.0f;
        float angleHorizontal = Mathf.PerlinNoise(transform.position.x * noiseScale, transform.position.z * noiseScale);
        float angleVertical = 0.5f;
        angleHorizontal = (angleHorizontal - 0.5f) * 2 * maxNextGenerationAngle;
        angleVertical = (angleVertical - 0.5f) * 2 * maxNextGenerationAngle;

        Debug.Log(angleHorizontal);

        // Altitude correction to clamp the curve
        float altitude = transform.position.y;
        if (altitude < (minAltitude + 100.0f))
        {
            float correctionAngle = Mathf.Abs(altitude - minAltitude) / 100.0f;
            transform.Rotate(new Vector3(-correctionAngle, 0.0f, 0.0f));
        }
        else if (altitude > (maxAltitude - 100.0f))
        {
            float correctionAngle = Mathf.Abs(altitude - maxAltitude) / 100.0f;
            transform.Rotate(new Vector3(correctionAngle, 0.0f, 0.0f));
        }

        transform.Rotate(0.0f, angleHorizontal, angleVertical);
        transform.Translate(0.0f, 0.0f, nextGenerationDistance);
    }
}
