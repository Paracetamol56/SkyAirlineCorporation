using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinNoiseGenerator
{
    public static float[,] GenerateNoiseMap(int mapLargeur, int mapHauteur, float scale,int octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[mapLargeur, mapHauteur];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;
        for (int y = 0; y < mapHauteur; y++)
        {
            for (int x = 0; x < mapLargeur; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / scale * frequency;
                    float sampleY = y / scale * frequency;

                    float perlinValeur = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValeur * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
                //Debug.Log(perlinValeur);
            }
        }
        for (int y = 0; y < mapHauteur; y++)
        {
            for (int x = 0; x < mapLargeur; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
