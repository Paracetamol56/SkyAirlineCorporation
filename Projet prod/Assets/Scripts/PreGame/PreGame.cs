using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGame : MonoBehaviour
{
    [SerializeField] GameObject[] planesSelection;
    [SerializeField] int rotationSpeed = 10;
    public int coef = 1;
    private GameObject currentPlaneShown;
    private int planeIndex = 0;
    private GameObject showPoint;

    // Start is called before the first frame update
    void Start()
    {
        showPoint = GameObject.Find("ShowPoint");
        planesSelection[planeIndex].transform.localScale = new Vector3(1, 1, 1) * coef;
        currentPlaneShown = Instantiate(planesSelection[planeIndex], showPoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        currentPlaneShown.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed); 
    }

    public void StartGame()
    {
        SaveCurrentPlane();
        ManagerScene.instance.SetMode(ManagerScene.GameMode.Freemode);
        ManagerScene.instance.LoadGameScene();
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void NextPlane()
    {
        planeIndex++;
        planeIndex = planeIndex > planesSelection.Length - 1 ? 0 : planeIndex;
        UpdateShownPlane();
    }

    public void PreviousPlane()
    {
        planeIndex--;
        planeIndex = planeIndex < 0 ? planesSelection.Length - 1 : planeIndex;
        UpdateShownPlane();
    }

    private void UpdateShownPlane()
    {
        Destroy(currentPlaneShown);
        currentPlaneShown = planesSelection[planeIndex];
        currentPlaneShown.transform.localScale = new Vector3(1, 1, 1) * coef;
        currentPlaneShown = Instantiate(currentPlaneShown, showPoint.transform);
    }

    private void SaveCurrentPlane()
    {
        // GameManager.instance.SetPlane(...);
        Debug.Log("Current plane has been saved in GameManager.");
    }
}
