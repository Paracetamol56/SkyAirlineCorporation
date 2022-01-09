using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    SceneIndex Level;
    public GameObject Player;
    private PlaneController Pcontroller;
    private DestructionManager Dmanager;

    private ManagerScene ms;
    void Start()
    {
        ms = ManagerScene.instance;
        Pcontroller = Player.GetComponent<PlaneController>();
        Dmanager = Player.GetComponent<DestructionManager>();
        gameObject.SetActive(false);
    }

    public void Pause()
    {

        Time.timeScale = 0;
        gameObject.SetActive(true);

    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void ResetPos()
    {
        Dmanager.drowned = false;
        Player.transform.position = new Vector3(0, 1000, 0);
        Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        Pcontroller.SetThrottleAverageSpeed(0f, 70f);
        Player.GetComponent<AudioSource>().volume = 0.08f;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        ms.SetMode(SceneIndex.PreGameScene);

        Level = ms.GetMode();
        StartCoroutine(ms.LoadGame(Level));
    }
}
