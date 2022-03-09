using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject airPlane;
    private PlaneController planeController;
    private DestructionHandler destructionManager;
    private ManagerScene managerScene;
    private AudioSource musicAudioSource;

    void Start()
    {
        managerScene = ManagerScene.instance;
        planeController = airPlane.GetComponent<PlaneController>();
        destructionManager = airPlane.GetComponent<DestructionHandler>();
        musicAudioSource = GameObject.Find("Song").GetComponent<AudioSource>();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ResetAirplanePosition()
    {
        destructionManager.drowned = false;
        planeController.RestAirplanePosition();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        managerScene.setCurrentSceneIndex(SceneIndex.MainMenu);
        managerScene.LoadGame();
    }

    public void MuteMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
    }
}
