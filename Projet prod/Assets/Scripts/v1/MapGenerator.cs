using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap,ColourMap,Mesh};
    public DrawMode drawMode;


    public int mapLargeur;
    public int mapHauteur;
    public float noiseScale;

    public int octavesnb;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public bool autoUpdate;

    
    public Vector2 offset;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = PerlinNoiseGenerator.GenerateNoiseMap(mapLargeur,mapHauteur,seed,noiseScale,octavesnb,persistance,lacunarity,offset);
        Color[] colourMap = new Color[mapLargeur * mapHauteur];
        for (int y = 0; y < mapHauteur; y++)
        {
            for(int x = 0; x < mapLargeur; x++)
            {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < regions.Length;i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapLargeur + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromCoulourMap(colourMap,mapLargeur,mapHauteur));
        }
        else if(drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromCoulourMap(colourMap, mapLargeur, mapHauteur));
        }
        //display.DrawNoiseMap(noiseMap);
    }
    private void OnValidate()
    {
        if (mapLargeur < 1)
        {
            mapLargeur = 1;
        }
        if (mapHauteur < 1)
        {
            mapHauteur = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octavesnb < 0)
        {
            octavesnb = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}