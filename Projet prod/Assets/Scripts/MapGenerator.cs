using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapLargeur;
    public int mapHauteur;
    public float noiseScale;

    public void GenerateMap()
    {
        float[,] noiseMap = PerlinNoiseGenerator.GenerateNoiseMap(mapLargeur,mapHauteur,noiseScale);
    }
}
