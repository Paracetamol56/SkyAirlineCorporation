using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathGenerator : MonoBehaviour
{
    [Header("Generation algorithm")]
    [SerializeField]
    private float maxNextGenerationAngle = 30.0f;
    [SerializeField]
    private float nextGenerationDistance = 200.0f;
    [SerializeField]
    private int gateSpacing = 50;
    private float pathSeed = 0.0f;

    [Header("Altitude limits")]
    [SerializeField]
    private float maxAltitude = 2000.0f;
    [SerializeField]
    private float minAltitude = 500.0f;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject pointPrefab;
    [SerializeField]
    private GameObject gatePrefab;

    [Header("Path length")]
    private List<GameObject> gateCircularBuffer = new List<GameObject>();
    [SerializeField]
    private uint gateCircularBufferSize = 4;

    [Header("Trail generation")]
    [SerializeField]
    private float movingStep = 2.0f;
    [SerializeField]
    private Transform trailGenerator;
    private List<Vector3> pointsPositions = new List<Vector3>();

    void Start()
    {
        // Set the random seed
        pathSeed = Time.time;
        // Srtating path
        for (int i = 0; i < gateCircularBufferSize; i++)
        {
            GenerateNextGate();
        }
    }

    private void Update()
    {
        if (pointsPositions.Count > 0)
        {
            trailGenerator.position = Vector3.MoveTowards(trailGenerator.position, pointsPositions[0], Time.deltaTime * movingStep);
            if (Vector3.Distance(trailGenerator.position, pointsPositions[0]) < 0.1f)
            {
                pointsPositions.RemoveAt(0);
            }
        }
    }

    private void GenerateNextGate()
    {
        for (int i = 0; i < gateSpacing; i++)
        {
            goToNextPoint();
        }
        // Instanciate a new gate
        GameObject newGate = Instantiate(gatePrefab, transform.position, transform.rotation) as GameObject;
        gateCircularBuffer.Add(newGate);
        if (gateCircularBuffer.Count > gateCircularBufferSize)
        {
            Destroy(gateCircularBuffer[0]);
            gateCircularBuffer.RemoveAt(0);
        }
    }

    private void goToNextPoint()
    {
        float angleHorizontal = Mathf.PerlinNoise(transform.position.x + pathSeed, transform.position.z + pathSeed);
        float angleVertical = Mathf.PerlinNoise(Time.time, 0.0f);
        angleHorizontal = (angleHorizontal - 0.5f) * 2 * maxNextGenerationAngle;
        angleVertical = (angleVertical - 0.5f) * 2 * maxNextGenerationAngle / 5.0f;

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

        pointsPositions.Add(transform.position);
    }
}
