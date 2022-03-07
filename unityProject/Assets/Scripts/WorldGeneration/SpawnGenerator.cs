using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnGenerator
{
    public static float GenerateSpawnMap(Vector2 sampleCentre, int coordX, int coordY, float sampleX, float sampleY)
    {
        float perlinValue;
        Vector2 Centre = new Vector2(49, 49);
        Vector2 currentPos = new Vector2(coordX + sampleCentre.x, coordY + sampleCentre.y);
        float distCentrCurrent = (Vector2.Distance(Centre, currentPos) < 10) ? 10 : Vector2.Distance(Centre, currentPos);
        float blendValue = 1;

        if (Mathf.Abs(Vector2.Distance(Centre, currentPos)) > 45.0f)
        {
            blendValue = 1;
        }
        else
        {
            blendValue = 50.0f / Mathf.Abs(Vector2.Distance(Centre, currentPos));
        }
        perlinValue = (distCentrCurrent < 45) ? 0 : ((1 - blendValue) * (-0.001f)) + ((Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1) * blendValue);
        return perlinValue;
    }
}
