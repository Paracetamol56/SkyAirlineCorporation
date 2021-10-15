using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapLargeur;
    public int mapHauteur;
    public float noiseScale;
    public int octavesnb;
    public float persistance;
    public float lacunarity;
    public bool autoUpdate;
    public void GenerateMap()
    {
        float[,] noiseMap = PerlinNoiseGenerator.GenerateNoiseMap(mapLargeur,mapHauteur,noiseScale,octavesnb,persistance,lacunarity);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}
