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
  private GameObject waypointUI;
  [SerializeField]
  private float heightOffset = 0.0f;
  [SerializeField]
  private float maxDistance = 1.0f;
  [SerializeField]
  private Color waypointColor = Color.red;
  [SerializeField]
  private string waypointName = "Waypoint";

  private TextMeshProUGUI distanceText;

  private void Awake()
  {
    // Get canevas object
    GameObject canvas = GameObject.Find("Canvas");
    // Instanciate waypointUI in the canevas
    waypointUI = Instantiate(waypointUI, canvas.transform);
    // Get distance text inside waypointUI called "Value (TMP)"
    distanceText = waypointUI.transform.Find("Value (TMP)").GetComponent<TextMeshProUGUI>();
    // Get the element called "Color" inside waypointUI and assign the color
    waypointUI.transform.Find("Color").GetComponent<Image>().color = waypointColor;
    // Get the element called "Name (TMP)" inside waypointUI and assign the name
    waypointUI.transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = waypointName;
  }

  private void Update()
  {
    // Get the distance between the player and the waypoint
    float distance = Vector3.Distance(MainCamera.transform.position, transform.position) / 16093;
    // If the distance is less than the max distance
    if (distance < maxDistance)
    {
      // If the waypoint if behind the player
      if (Vector3.Dot(transform.position + new Vector3(0, heightOffset, 0) - MainCamera.transform.position, MainCamera.transform.forward) < 0)
      {
        // Hide the overlay
        waypointUI.gameObject.SetActive(false);
      }
      else
      {
        // Show the overlay
        waypointUI.gameObject.SetActive(true);
        Vector3 screenPos = MainCamera.WorldToScreenPoint(transform.position + new Vector3(0, heightOffset, 0));
        screenPos.z = 0;
        waypointUI.transform.position = screenPos;
        distanceText.text = distance.ToString("F2");
      }
    }
    else
    {
      // Hide the overlay
      waypointUI.gameObject.SetActive(false);
    }
  }
}