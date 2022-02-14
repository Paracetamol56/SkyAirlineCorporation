using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlText : MonoBehaviour
{
    KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        key = (KeyCode)PlayerPrefs.GetInt(gameObject.name);
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = key.ToString();
    }

    public void ResetText()
    {
        key = (KeyCode)PlayerPrefs.GetInt(gameObject.name);
        if (key.ToString() != gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text)
        {
            gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = key.ToString();
        }
    }

}
