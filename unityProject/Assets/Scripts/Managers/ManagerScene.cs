using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneIndex
{
    MainMenu = 0,
    DeathScene = 1,
    Freemode = 2,
    FFplane = 3,
    //Delivery = 4,
    Freestyle = 4
}

public class ManagerScene : MonoBehaviour
{
    ////////////SingleTon/////////////
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
    public SceneIndex currentSceneIndex
    {
        get
        {
            return currentSceneIndex;
        }
        set
        {
            currentSceneIndex = value;
            StartCoroutine(LoadGame());
        }
    }

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

    public void Quit()
    {
        Application.Quit();
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public IEnumerator LoadGame()
    {
        Debug.Log("Loading scene " + (int)currentSceneIndex);

        // Material reset
        PrimaryMaterial.color = PrimaryColor;
        SecondaryMaterial.color = SecondaryColor;

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
            // case SceneIndex.Delivery:
            //     {
            //         SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.Delivery);
            //         // Change the material color
            //         PrimaryMaterial.color = Color.green;
            //         SecondaryMaterial.color = Color.black;
            //         break;
            //     }
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
        yield return new WaitForSeconds(1f);
    }
}
