using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class Waypoint : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;
    [SerializeField]
    private TextMeshProUGUI distanceText;
    [SerializeField]
    private RectTransform Overlay;

    private void Update()
    {
        // If the waypoint if behind the player
        if (Vector3.Dot(transform.position - MainCamera.transform.position, MainCamera.transform.forward) < 0)
        {
            // Hide the overlay
            Overlay.gameObject.SetActive(false);
        }
        else
        {
            // Show the overlay
            Overlay.gameObject.SetActive(true);
            Vector3 screenPos = MainCamera.WorldToScreenPoint(transform.position);
            screenPos.z = 0;
            Overlay.transform.position = screenPos;
            distanceText.text = (Vector3.Distance(transform.position, MainCamera.transform.position) / 16093).ToString("F2");
        }
    }
}