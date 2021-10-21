using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangerDetection : MonoBehaviour
{
    private bool canJoin = false;
    [SerializeField]
    private Text canJoinText;
    
    private void OnTriggerEnter(Collider other)
    {
        canJoin = true;
        switch (transform.name)
        {
            case "HangerLivraison":
                Debug.Log("Call SceneManager : arg HangerLivraison");
                canJoinText.text = "Appuyez sur ENTREE pour lancer la partie en mode : Livraison";
                // Setup the current gametype in the GameManager
                break;
            case "HangerCanadaire":
                Debug.Log("Call SceneManager : arg HangerCanadaire");
                canJoinText.text = "Appuyez sur ENTREE pour lancer la partie en mode : Canadaire";
                // Setup the current gametype in the GameManager
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && canJoin)
        {
            // call the fonction loadGameScene of SceneManager
            Debug.Log("waw je lance le jeu");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(timeForJoin());
    }

    IEnumerator timeForJoin()
    {
        yield return new WaitForSeconds(5.0f);
        canJoin = false;
        canJoinText.text = "";
    }
}
