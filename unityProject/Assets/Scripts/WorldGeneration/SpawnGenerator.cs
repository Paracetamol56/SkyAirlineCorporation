using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnGenerator
{
    public static float GenerateSpawnMap(ref float perlinValue)
    {

        float newPerlinValue;
        int x;
        int y;
        perlinValue = 0.5f * 2 - 1;
        newPerlinValue = perlinValue;
        return newPerlinValue;
    }
}
