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
            message.text= "Start FireFighting GameMode\n(press F)";
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            message.text= "";
        }
    }
}
