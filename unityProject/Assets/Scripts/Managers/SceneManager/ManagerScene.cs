using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerScene : MonoBehaviour
{
    //////////////SingleTon/////////////
    public static ManagerScene instance;
    public GameObject loadingScreen;
    public Image progressBar;

    //private void MakeSingleton()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    void Awake()
    {
        //MakeSingleton();

        SceneManager.LoadSceneAsync((int)GameMode.PreGameScene, LoadSceneMode.Additive);
    }

    /////////////////////////////////////

    //enum
    public enum GameMode
    {
        PreGameScene = 0,
        Presentation = 1,
        LoadingScreen = 2,
        Freemode = 3,
        Delivery = 4,
        //FFPlane = fire-fighting plane=canadair
        FFplane = 5

    }


    //variables
    public GameMode Mode;

    void Start()
    {
        Mode = GameMode.Freemode;
    }

    public void SetMode(GameMode value)
    {
        Mode = value;
    }

    public GameMode GetMode()
    {
        return Mode;
    }

    public IEnumerator LoadGameScene()
    {
        switch (Mode)
        {
            case GameMode.Freemode:
                yield return new WaitForSeconds(3.0f);
                SceneManager.LoadScene("FreeMode");
                break;
            case GameMode.Delivery:
                yield return new WaitForSeconds(3.0f);
                SceneManager.LoadScene("Delivery");
                break;
            case GameMode.FFplane:
                yield return new WaitForSeconds(3.0f);
                SceneManager.LoadScene("FFPlane");
                break;
            case GameMode.Presentation:
                yield return new WaitForSeconds(3.0f);
                SceneManager.LoadScene("Presentation");
                break;
            default:
                break;
        }
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)GameMode.PreGameScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)GameMode.Presentation, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)GameMode.LoadingScreen, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
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

                progressBar.fillAmount = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }
        }

        //loadingScreen.gameObject.SetActive(false);
    }
}
