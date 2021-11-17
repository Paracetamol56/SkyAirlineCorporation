using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashboardUI : MonoBehaviour
{
    // Air plane reference
    [SerializeField]
    private GameObject airPlane;

    // Compass reference
    [Header("Compass")]
    [SerializeField]
    private Image compass;
    [SerializeField]
    private float orientationOffset;

    // Speedometer reference
    [Header("Speedometer")]
    [SerializeField]
    private Image speedometer;
    [SerializeField]
    private GameObject speedText;

    // RPM counter reference
    [Header("RPM counter")]
    [SerializeField]
    private Image rpmCounter;

    // Altivariometer reference
    [Header("Altivariometer")]
    [SerializeField]
    private Image altivariometer;

    // Altimeter reference
    [Header("Altimeter")]
    [SerializeField]
    private Image altimeterFirstHandPointer;
    [SerializeField]
    private Image altimeterSecondHandPointer;
    [SerializeField]
    private GameObject altitudeText;

    private Rigidbody airPlaneRigidbody;
    private Transform airPlaneTransform;

    private void Start()
    {
        if (airPlane)
        {
            airPlaneRigidbody = airPlane.GetComponent<Rigidbody>();
            airPlaneTransform = airPlane.transform;
        }
    }

    /// <summary>
    /// Update all images rotation/state acording to airplane data
    /// </summary>
    private void Update()
    {
        // Compass
        compass.rectTransform.localRotation = Quaternion.Euler(0, 0, airPlaneTransform.rotation.eulerAngles.y + orientationOffset);

        // Speedometer


        // RPM counter


        // Altivariometer


        // Altimeter
    }
}
