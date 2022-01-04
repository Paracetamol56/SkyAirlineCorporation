using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCaller : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneIndex mode;
    public bool CanChangeScene;
    private ManagerScene ms;

    // Update is called once per frame

    void Start()
    {
        ms = ManagerScene.instance;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CanChangeScene == true)
            {
                SceneIndex Level;
                ms.SetMode(mode);
                Level = ms.GetMode();
                StartCoroutine(ms.LoadGame(Level));
            }
        }
    }
}
