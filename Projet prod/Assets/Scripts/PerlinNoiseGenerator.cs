using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinNoiseGenerator
{
    public static float[,] GenerateNoiseMap(int mapLargeur, int mapHauteur, float scale)
    {
        float[,] noiseMap = new float[mapLargeur, mapHauteur];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        for (int y = 0; y < mapHauteur; y++)
        {
            for (int x = 0; x < mapLargeur; x++)
            {
                float sampleX = x;
                float sampleY = y;

                float perlinValeur = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValeur;
            }

        }
        return noiseMap;
    }
}
