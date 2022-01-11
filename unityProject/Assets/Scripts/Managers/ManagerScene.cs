using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScene : MonoBehaviour
{
    ////////////SingleTon/////////////
    public static ManagerScene instance;

    // Plane material
    [SerializeField]
    private Material PrimaryMaterial;
    [SerializeField]
    private Material SecondaryMaterial;

    private Color PrimaryColor;
    private Color SecondaryColor;

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        MakeSingleton();
    }

    public void Start()
    {
        SetMode(SceneIndex.PreGameScene);

        PrimaryColor = PrimaryMaterial.color;
        SecondaryColor = SecondaryMaterial.color;
    }

    //variables
    public SceneIndex Mode;

    public void SetMode(SceneIndex value)
    {
        Mode = value;
    }

    public SceneIndex GetMode()
    {
        return Mode;
    }

    public void Quit()
    {
        Application.Quit();
    }


    //////////Test Nouveau SceneManager n°1/////////////

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public IEnumerator LoadGame(SceneIndex Level)
    {
        Debug.Log("changement vers " + (int)Level);

        // Material reset
        PrimaryMaterial.color = PrimaryColor;
        SecondaryMaterial.color = SecondaryColor;

        switch (Level)
        {

            //Loader Synchrone
            case SceneIndex.PreGameScene:
                SetMode(SceneIndex.PreGameScene);
                SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.PreGameScene);
                break;

            case SceneIndex.Freemode:
                SetMode(SceneIndex.Freemode);
                SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.Freemode);

                break;

            case SceneIndex.FFplane:
                SetMode(SceneIndex.FFplane);
                SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.FFplane);
                // Changement de la couleur des material
                {
                    // Change the material color
                    PrimaryMaterial.color = Color.yellow;
                    SecondaryMaterial.color = Color.red;
                }
                break;

            case SceneIndex.DeathScene:
                SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.DeathScene);
                break;

            default:
                SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.Freemode);
                break;

        }

        yield return new WaitForSeconds(1f);
    }



    //truc sahel

    //public IEnumerator LoadGameScene(string Mode)
    //{
    //    switch (Mode)
    //    {
    //        case "Freemode":/*GameMode.Freemode:*/
    //            yield return new WaitForSeconds(.1f);
    //            SceneManager.LoadScene("FreeMode");
    //            break;
    //        case "Delivery":/*GameMode.Delivery:*/
    //            yield return new WaitForSeconds(.1f);
    //            SceneManager.LoadScene("Delivery");
    //            break;
    //        case "FFplane":/*GameMode.FFplane:*/
    //            yield return new WaitForSeconds(.1f);
    //            SceneManager.LoadScene("FFPlane");
    //            break;
    //        case "Presentation":/*GameMode.Presentation:*/
    //            yield return new WaitForSeconds(.1f);
    //            SceneManager.LoadScene("Presentation");
    //            break;
    //        default:
    //            yield return new WaitForSeconds(.1f);
    //            SceneManager.LoadScene("FreeMode");
    //            break;
    //    }
    //}
}
