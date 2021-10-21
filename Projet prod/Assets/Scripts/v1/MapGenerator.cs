using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap,ColourMap,Mesh};
    public DrawMode drawMode;

    const int mapChunkSize=241;
    [Range(0,6)]
    public int levelOfDetail;
    public float noiseScale;
    
    public int octavesnb;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;
    public bool autoUpdate;

    
    public Vector2 offset;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = PerlinNoiseGenerator.GenerateNoiseMap(mapChunkSize,mapChunkSize,seed,noiseScale,octavesnb,persistance,lacunarity,offset);
        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for(int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < regions.Length;i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
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
            display.DrawTexture(TextureGenerator.TextureFromCoulourMap(colourMap,mapChunkSize,mapChunkSize));
        }
        else if(drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromCoulourMap(colourMap, mapChunkSize, mapChunkSize)) ;
        }
        //display.DrawNoiseMap(noiseMap);
    }
    private void OnValidate()
    {
        
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