using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject pointPrefab;

    void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            // generate a new path
            goToNextPoint();
            GameObject newPoint = Instantiate(pointPrefab, transform.position, transform.rotation);
            //newPoint.transform.parent = transform;
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
        transform.Translate(distance, 0.0f, 0.0f);
    }
}
