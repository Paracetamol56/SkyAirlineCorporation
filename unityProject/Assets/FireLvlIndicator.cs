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
        int Nbflames = scriptFire.nbflames;
        int max = scriptFire.GetNbFlames();

        int pourcentage = Nbflames*100/max;
        int affichage = 100- pourcentage;
        if(affichage < 0) affichage=0;
        GetComponent<Text>().text = "Feu éteint à "+affichage+ " %";

    }


}
