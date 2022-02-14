using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPause : MonoBehaviour
{

    public GameObject PauseCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PauseCanvas.activeSelf == false)
        {
            PauseCanvas.GetComponent<PauseMenu>().Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseCanvas.activeSelf == true)
        {
            PauseCanvas.GetComponent<PauseMenu>().Resume();
        }
    }
}
