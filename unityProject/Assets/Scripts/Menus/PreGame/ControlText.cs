using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        KeyCode key = (KeyCode)PlayerPrefs.GetInt(gameObject.name);
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = key.ToString();
    }
}
