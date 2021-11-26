using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    //////////////SingleTon/////////////
    public static ManagerScene instance;

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

    void Awake()
    {
        MakeSingleton();

        SceneManager.LoadSceneAsync((int)GameMode.PreGameScene, LoadSceneMode.Additive);
    }

    /////////////////////////////////////

    //enum
    public enum GameMode
    {
        PreGameScene,
        Presentation,
        LoadingScreen,
        Freemode,
        Delivery,
        //FFPlane = fire-fighting plane=canadair
        FFplane

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

    public void BacktoMenu()
    {
        SceneManager.LoadScene("PreGameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void Loadgame()
    {
        SceneManager.UnloadSceneAsync((int)GameMode.PreGameScene);
        SceneManager.LoadSceneAsync((int)GameMode.Presentation, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync((int)GameMode.LoadingScreen, LoadSceneMode.Additive);
    }
}
