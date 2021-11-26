using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnGenerator
{
    public static float GenerateSpawnMap(ref float perlinValue, Vector2 sampleCentre, int sampleX, int sampleY, int mapWidth, int mapHeight)
    {

        //int x;
        //int y;
        if (Mathf.Abs(sampleY) + Mathf.Abs(sampleCentre.y) < 100 && Mathf.Abs(sampleCentre.x) + Mathf.Abs(sampleX) < 100)
        {
            if (Mathf.Abs(sampleY) + Mathf.Abs(sampleCentre.y) > 25 && Mathf.Abs(sampleCentre.x) + Mathf.Abs(sampleX) > 25)
            {
                perlinValue = 0.5f * 2 - 1;

            }

        }
        if (sampleCentre.x + sampleCentre.y == 125)
        {
            Debug.Log(mapWidth);
            Debug.Log(mapHeight);
        }

        //return newPerlinValue;
        //for (int i = 0; i < size; i++)
        //{
        //    for (int j = 0; j < size; j++)
        //    {
        //        float x = i / (float)size * 2 - 1;
        //        float y = j / (float)size * 2 - 1;

        //        float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
        //        map[i, j] = Evaluate(value);
        //    }
        //}
        return perlinValue;
    }

    static float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;
        //Debug.Log(Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a)));
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }

}
