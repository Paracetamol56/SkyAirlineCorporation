using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneScript : MonoBehaviour
{
    SceneIndex Level;

    private ManagerScene ms;
    void Start()
    {
        ms = ManagerScene.instance;
    }

    public void RestartGame()
    {

        ms.SetMode(ms.GetMode());
        //ms.SetMode(SceneIndex.Freemode);

        Level = ms.GetMode();
        Debug.Log(Level);
        StartCoroutine(ms.LoadGame(Level));
    }

    public void ExitGame()
    {
        ms.SetMode(SceneIndex.PreGameScene);

        Level = ms.GetMode();
        StartCoroutine(ms.LoadGame(Level));
    }
}
