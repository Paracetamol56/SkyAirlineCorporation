using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPause : MonoBehaviour
{

    public GameObject PauseCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseCanvas.GetComponent<PauseMenu>().Pause();
        }
    }
}
