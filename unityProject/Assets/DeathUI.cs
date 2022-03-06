using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{

    private ManagerScene ms;
    // Start is called before the first frame update
    void Start()
    {
        ms = GameObject.Find("SceneManager").GetComponent<ManagerScene>();
    }

    public void Restart()
    {
        ms.loadlastscene();
    }
    public void BacktoMenu()
    {
        ms.setCurrentSceneIndex(SceneIndex.MainMenu);
        ms.LoadGame();
    }
}
