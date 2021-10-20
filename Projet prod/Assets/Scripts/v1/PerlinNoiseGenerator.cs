using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinNoiseGenerator
{
    public static float[,] GenerateNoiseMap(int mapLargeur, int mapHauteur,int seed, float scale, int octaves, float persistance, float lacunarity,Vector2 offset)
    {
        float[,] noiseMap = new float[mapLargeur, mapHauteur];
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000)+offset.x;
            float offsetY = prng.Next(-100000, 100000)+offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapLargeur / 2f;
        float halfHeight = mapHauteur / 2f;
        for (int y = 0; y < mapHauteur; y++)
        {
            for (int x = 0; x < mapLargeur; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale * frequency+octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency+octaveOffsets[i].y;

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
