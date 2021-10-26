using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    private static GlobalGameManager instance;

    private listOfPlanes selectedPlane;
    public enum listOfPlanes
    {
        basic_plane,
        canadaire_plane,
        delivery_plane
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Double instance GlobalGameManager");
            return;
        }
        instance = this;
    }

    public static GlobalGameManager GetInstance()
    {
        return instance;
    }

    public listOfPlanes GetSelectedPlane()
    {
        return selectedPlane;
    }

    public void SetSelectedPlane(listOfPlanes plane)
    {
        selectedPlane = plane;
    }
}
