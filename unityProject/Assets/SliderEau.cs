using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderEau : MonoBehaviour
{
    public GameObject Player;
    public Slider jauge;

    void Update()
    {
        jauge.value = Player.GetComponent<CanadaireObjectController>().setWater;
    }
}
