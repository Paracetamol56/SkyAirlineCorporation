using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class ManagerScene : MonoBehaviour
{
    //////////////SingleTon/////////////
    public static ManagerScene instance;

    private void MakeSingleton()
    {
        if (instance !=null)
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
    }

    /////////////////////////////////////

    //enum
    public enum GameMode
    {
        Freemode,
        Delivery,
        //FFPlane = fire-fighting plane=canadair
        FFplane

    }

    //variables
    private GameMode Mode;


    void Start()
    {
        Mode = GameMode.Freemode;
    }


    public  void SetMode(GameMode value)
    {
        Mode =  value;
    }

    public GameMode GetMode()
    {
        return Mode;
    }

    public void LoadGameScene()
    {
        switch(Mode)
        {
            case GameMode.Freemode: 
                SceneManager.LoadScene("FreeMode");
                break;
            case GameMode.Delivery:
                SceneManager.LoadScene("Delivery");
                break;
            case GameMode.FFplane:
                SceneManager.LoadScene("FFPlane");
                break;
            default:
                break;

        }
    }

    
}
