using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneLoader : MonoBehaviour
{
    private ManagerScene ms;

    // Update is called once per frame

    void Start()
    {
        ms = ManagerScene.instance;
        StartCoroutine(DeathLoading());
    }


    IEnumerator DeathLoading()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(ms.LoadGame(SceneIndex.DeathScene));
    }
}
