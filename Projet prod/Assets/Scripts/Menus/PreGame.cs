using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGame : MonoBehaviour
{
    [SerializeField] GameObject[] planesSelection;
    private GameObject currentPlaneSelected;
    private int planeIndex = 0;
    private GameObject showPoint;
    // Start is called before the first frame update
    void Start()
    {
        showPoint = GameObject.Find("ShowPoint");
        currentPlaneSelected = planesSelection[planeIndex];
        currentPlaneSelected.transform.localScale = new Vector3(1, 1, 1) * 5000;
        Instantiate(currentPlaneSelected, showPoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // SceneManager Load Scene 
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NextPlane()
    {

    }

    public void PreviousPlane()
    {

    }
}
