using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    ///////Test n°1//////////////
    [SerializeField]
    private Image progressBar;
    
    private ManagerScene ms;
    private GlobalGameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    
        ms = ManagerScene.instance;
    }
    
    IEnumerator LoadAsyncOperation()
    {
        //decommenter quand il y aura les modes de jeux
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync((int)ms.GetMode());
    
        //AsyncOperation gameLevel = SceneManager.LoadSceneAsync(1);
    
        while (!gameLevel.isDone)
        {
            float progress = Mathf.Clamp01(gameLevel.progress / .9f);
            progressBar.fillAmount = progress;
            yield return new WaitForEndOfFrame();
        }
    
    }
}
