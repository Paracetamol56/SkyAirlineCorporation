using UnityEngine;

public class PreGame : MonoBehaviour
{
    [SerializeField] GameObject[] planesSelection;
    [SerializeField] int rotationSpeed = 10;
    public int coef = 1;
    private GameObject currentPlaneShown;
    private int planeIndex = 0;
    private GameObject showPoint;
    private GlobalGameManager.listOfPlanes currentPlaneType;
    private Quaternion planeAngle;

    private bool isPaint = false;
    public GameObject ColorPickedPrefab;
    private ColorPickerTriangle CPprime;
    private ColorPickerTriangle CPsecond;
    public Material primeColor;
    public Material secondColor;
    private GameObject pickerPrim;
    private GameObject pickerSecond;

    // Instances of managers
    private GlobalGameManager gm;
    private ManagerScene managerScene;

    void Start()
    {
        showPoint = GameObject.Find("ShowPoint");
        planesSelection[planeIndex].transform.localScale = new Vector3(1, 1, 1) * coef;
        currentPlaneShown = Instantiate(planesSelection[planeIndex], showPoint.transform);
        managerScene = GameObject.Find("SceneManager").GetComponent<ManagerScene>(); 
    }

    void Update()
    {
        currentPlaneShown.transform.position = showPoint.transform.position;
        currentPlaneShown.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        planeAngle = currentPlaneShown.transform.rotation;
        if (isPaint)
        {
            primeColor.color = CPprime.TheColor;
            secondColor.color = CPsecond.TheColor;
        }

    }

    public void StartGame()
    {
        SaveCurrentPlane();

        managerScene.setCurrentSceneIndex(SceneIndex.Freemode);
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
        if (planeIndex == 1)
            currentPlaneShown.transform.localScale = new Vector3(1, 1, 1);
        currentPlaneShown = Instantiate(currentPlaneShown, showPoint.transform);
        currentPlaneShown.transform.rotation = planeAngle;

        switch (planeIndex)
        {
            case 0:
                currentPlaneType = GlobalGameManager.listOfPlanes.canadaire_plane;
                break;

            case 1:
                currentPlaneType = GlobalGameManager.listOfPlanes.delivery_plane;
                break;

            case 2:
                currentPlaneType = GlobalGameManager.listOfPlanes.basic_plane;
                break;

            default:
                currentPlaneType = GlobalGameManager.listOfPlanes.basic_plane;
                break;
        }
    }

    private void SaveCurrentPlane()
    {
        // GameManager.instance.SetPlane(...);
        Debug.Log("Current plane has been saved in GameManager.");
    }

    void OnMouseDown()
    {


    }

    public void ChangeColor()
    {
        isPaint = !isPaint;

        if (isPaint)
        {
            pickerPrim = (GameObject)Instantiate(ColorPickedPrefab, showPoint.transform.position + new Vector3(-1, -1, 0) * 0.3f, Quaternion.identity);
            pickerSecond = (GameObject)Instantiate(ColorPickedPrefab, showPoint.transform.position + new Vector3(1, -1, 0) * 0.3f, Quaternion.identity);
            pickerPrim.transform.localScale = Vector3.one * 0.3f;
            pickerSecond.transform.localScale = Vector3.one * 0.3f;
            pickerPrim.transform.LookAt(Camera.main.transform);
            pickerSecond.transform.LookAt(Camera.main.transform);
            pickerPrim.transform.rotation = new Quaternion(0, 180, 0, 0);
            pickerSecond.transform.rotation = new Quaternion(0, -180, 0, 0);
            CPprime = pickerPrim.GetComponent<ColorPickerTriangle>();
            CPsecond = pickerSecond.GetComponent<ColorPickerTriangle>();
            CPprime.SetNewColor(primeColor.color);
            CPsecond.SetNewColor(secondColor.color);
        }
        else
        {
            Destroy(pickerPrim);
            Destroy(pickerSecond);
        }
    }

}
