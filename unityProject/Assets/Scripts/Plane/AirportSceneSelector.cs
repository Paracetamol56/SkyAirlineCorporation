using UnityEngine;
using UnityEngine.UI;

class AirportSceneSelector : MonoBehaviour
{
    [SerializeField]
    private Text airportMessage;

    private bool canJoinGameMode = false;
    private ManagerScene managerScene;
    private SceneIndex sceneToLoad;

    private void Start()
    {
        managerScene = GameObject.Find("SceneManager").GetComponent<ManagerScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verify the name of other
        if (other.gameObject.name == "FireFightingPlaneAirport")
        {
            canJoinGameMode = true;
            airportMessage.text = "Press F to join the Fire Fighting Plane Game Mode";
            sceneToLoad = SceneIndex.FFplane;
        }
        else if (other.name == "DeliveryAirport")
        {
            canJoinGameMode = true;
            airportMessage.text = "Press F to join the Delivery Game Mode";
            sceneToLoad = SceneIndex.Delivery;
        }
        else if (other.name == "FreestyleAirport")
        {
            canJoinGameMode = true;
            airportMessage.text = "Press F to join the Freestyle Game Mode";
            sceneToLoad = SceneIndex.Freestyle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canJoinGameMode = false;
        airportMessage.text = "";
        sceneToLoad = SceneIndex.Freemode;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canJoinGameMode == true)
            {
                managerScene.setCurrentSceneIndex(sceneToLoad);
                managerScene.LoadGame();
            }
        }
    }
}