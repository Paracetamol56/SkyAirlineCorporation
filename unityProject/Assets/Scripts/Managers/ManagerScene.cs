using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndex
{
    MainMenu = 0,
    DeathScene = 1,
    Freemode = 2,
    FFplane = 3,
    Freestyle = 4,
    Delivery = 5,
}

public class ManagerScene : MonoBehaviour
{
    public static ManagerScene instance;

    // Plane material
    [Header("Plane Material")]
    [SerializeField]
    private Material PrimaryMaterial;
    [SerializeField]
    private Material SecondaryMaterial;

    // Variables to store the colors during all the game session
    private Color PrimaryColor;
    private Color SecondaryColor;

    // Make getter and setter for the current scene index
    [HideInInspector]
    private SceneIndex currentSceneIndex;
    private SceneIndex lastSceneIndex;

    private void Awake()
    {
        // Make singleton
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        currentSceneIndex = SceneIndex.MainMenu;


        // Store materials color choosed by the user
        PrimaryColor = PrimaryMaterial.color;
        SecondaryColor = SecondaryMaterial.color;
    }

    public SceneIndex getCurrentSceneIndex()
    {
        return currentSceneIndex;
    }

    public void setCurrentSceneIndex(SceneIndex sceneIndex)
    {
        currentSceneIndex = sceneIndex;
        Debug.Log("Current scene index: " + currentSceneIndex);
        LoadGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLastScene()
    {
        currentSceneIndex = lastSceneIndex;
    }

    private void LoadGame()
    {
        Debug.Log("Loading scene " + (int)currentSceneIndex);

        // Material reset
        PrimaryMaterial.color = PrimaryColor;
        SecondaryMaterial.color = SecondaryColor;

        // Store the last scene index
        lastSceneIndex = currentSceneIndex;

        switch (currentSceneIndex)
        {
            case SceneIndex.MainMenu:
                {
                    SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.MainMenu);
                    break;
                }
            case SceneIndex.DeathScene:
                {
                    SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.DeathScene);
                    break;
                }
            case SceneIndex.Freemode:
                {
                    SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.Freemode);
                    // Change the material color
                    PrimaryMaterial.color = Color.white;
                    SecondaryMaterial.color = Color.blue;
                    break;
                }
            case SceneIndex.FFplane:
                {
                    SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.FFplane);
                    // Change the material color
                    PrimaryMaterial.color = Color.yellow;
                    SecondaryMaterial.color = Color.red;
                    break;
                }
            case SceneIndex.Delivery:
                {
                    SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.Delivery);
                    // Change the material color
                    PrimaryMaterial.color = Color.green;
                    SecondaryMaterial.color = Color.black;
                    break;
                }
            case SceneIndex.Freestyle:
                {
                    SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.Freestyle);
                    // Change the material color
                    PrimaryMaterial.color = Color.green;
                    SecondaryMaterial.color = Color.black;
                    break;
                }
            default:
                {
                    SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.Freemode);
                    break;
                }
        }
    }
}
