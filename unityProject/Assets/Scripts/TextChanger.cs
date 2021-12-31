using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text message;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (gameObject.name == "AeroportFFPlane")
                message.text = "Start FireFighting GameMode\n(press F)";
            if (gameObject.name == "AeroportVoltige")
                message.text = "Start Voltige GameMode\nComing Soon";
            if (gameObject.name == "AeroportLivraison")
                message.text = "Start Livraison GameMode\nComing Soon";
            
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            message.text = "";
        }
    }
}
