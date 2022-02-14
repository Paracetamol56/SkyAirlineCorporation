using UnityEngine;

public class PauseListener : MonoBehaviour
{
    GameObject pauseMenu;

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                // Hide the UI
                pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                // Show the UI
                pauseMenu.SetActive(true);
            }
        }
    }
}