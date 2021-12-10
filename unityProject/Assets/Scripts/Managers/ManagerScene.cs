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
    public GameObject loadingScreen;
    public Slider bar;
    public TextMeshProUGUI textField;

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
        loadingScreen.SetActive(false);
        SetMode(SceneIndex.PreGameScene);
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

        loadingScreen.SetActive(true);

        switch (Level)
        {
            //Loader Synchrone
            case SceneIndex.Freemode:
                SceneManager.LoadScene(sceneBuildIndex: (int)SceneIndex.Freemode);
                StartCoroutine(GetSceneLoadProgress());
                break;

            case SceneIndex.Delivery:
                scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.Delivery, LoadSceneMode.Additive));
                StartCoroutine(GetSceneLoadProgress());
                break;

            case SceneIndex.FFplane:
                scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.FFplane, LoadSceneMode.Additive));
                StartCoroutine(GetSceneLoadProgress());
                break;

            case SceneIndex.Presentation:
                scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.Presentation, LoadSceneMode.Additive));
                StartCoroutine(GetSceneLoadProgress());
                break;

            default:
                scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.Freemode, LoadSceneMode.Additive));
                StartCoroutine(GetSceneLoadProgress());
                break;

            //Loader Asynchrone
            //case SceneIndex.Freemode:
            //    Debug.Log("branlette");
            //    scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.Freemode, LoadSceneMode.Additive));
            //    StartCoroutine(GetSceneLoadProgress());
            //    break;           
            //
            //case SceneIndex.Delivery:
            //   scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.Delivery, LoadSceneMode.Additive));          
            //   StartCoroutine(GetSceneLoadProgress());
            //   break;
            //
            //case SceneIndex.FFplane:
            //   scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.FFplane, LoadSceneMode.Additive));        
            //   StartCoroutine(GetSceneLoadProgress());
            //   break;
            //
            //case SceneIndex.Presentation:
            //   scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.Presentation, LoadSceneMode.Additive));
            //   StartCoroutine(GetSceneLoadProgress());
            //   break;
            //
            //default:
            //    scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.Freemode, LoadSceneMode.Additive));
            //    StartCoroutine(GetSceneLoadProgress());
            //    break;
        }

        yield return new WaitForSeconds(1f);
    }

    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                bar.value = Mathf.RoundToInt(totalSceneProgress);

                textField.text = string.Format("Loading Environments: {0}%", totalSceneProgress);

                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
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
