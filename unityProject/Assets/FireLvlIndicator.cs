using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireLvlIndicator : MonoBehaviour
{
    public GameObject FireGenerator;
    private GenerateFire scriptFire;

    public void Start()
    {
        scriptFire = FireGenerator.GetComponent<GenerateFire>();
    }

    public void Update()
    {
        int size = scriptFire.ListLenght();
        int max = 50;

        int pourcentage = size*100/max;
        int affichage = 100- pourcentage;
        GetComponent<Text>().text = "Feu éteint à "+affichage+ " %";

    }


}
