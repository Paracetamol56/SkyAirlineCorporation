using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject airPlane;
    private PlaneController planeController;
    private DestructionManager destructionManager;
    private ManagerScene managerScene;

    void Start()
    {
        managerScene = ManagerScene.instance;
        planeController = airPlane.GetComponent<PlaneController>();
        destructionManager = airPlane.GetComponent<DestructionManager>();
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
        destructionManager.drowned = false;
        planeController.RestAirplanePosition();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        managerScene.setCurrentSceneIndex(SceneIndex.MainMenu);
    }
}
