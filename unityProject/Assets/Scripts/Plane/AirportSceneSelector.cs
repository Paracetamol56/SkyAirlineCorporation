using UnityEngine;
using UnityEngine.UI;

class AirportSceneSelector : MonoBehaviour
{
    private bool canJoinGameMode = false;
    private ManagerScene managerScene;
    private SceneIndex SceneToLoad;
    private Text airportText;

    private void Start()
    {
        managerScene = ManagerScene.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verify the name of other
        if (other.name == "FireFightingPlaneAirport")
        {
            canJoinGameMode = true;
            airportText.text = "Press F to join the Fire Fighting Plane Game Mode";
            managerScene.currentSceneIndex = SceneIndex.FFplane;
        }
        // else if (other.name == "DeliveryAirport")
        // {
        //     canJoinGameMode = true;
        //     airportText.text = "Press F to join the Delivery Game Mode";
        //     managerScene.currentSceneIndex = SceneIndex.Delivery;
        // }
        else if (other.name == "FreestyleAirport")
        {
            canJoinGameMode = true;
            airportText.text = "Press F to join the Freestyle Game Mode";
            managerScene.currentSceneIndex = SceneIndex.Freestyle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canJoinGameMode = false;
        SceneToLoad = SceneIndex.Freemode;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canJoinGameMode == true)
            {
                managerScene.currentSceneIndex = SceneToLoad;
            }
        }
    }
}