using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnGenerator
{
<<<<<<< Updated upstream
    public static float GenerateSpawnMap(ref float perlinValue, Vector2 sampleCentre, int sampleX, int sampleY, int mapWidth, int mapHeight)
=======
    public static float GenerateSpawnMap(ref float perlinValue,Vector2 sampleCentre,int coordX,int coordY,float sampleX,float sampleY)
>>>>>>> Stashed changes
    {

        //int x;
        //int y;
<<<<<<< Updated upstream
        if (Mathf.Abs(sampleY) + Mathf.Abs(sampleCentre.y) < 100 && Mathf.Abs(sampleCentre.x) + Mathf.Abs(sampleX) < 100)
=======
        if (Mathf.Abs(coordY) +Mathf.Abs(sampleCentre.y)<110&&Mathf.Abs(sampleCentre.x)+Mathf.Abs(coordX) <110)
>>>>>>> Stashed changes
        {
            if (Mathf.Abs(coordY) + Mathf.Abs(sampleCentre.y) > 15 && Mathf.Abs(sampleCentre.x) + Mathf.Abs(coordX) > 15)
            {
<<<<<<< Updated upstream
                perlinValue = 0.5f * 2 - 1;

=======
                Vector2 Centre = new Vector2(62, 62);
                Vector2 currentPos = new Vector2(coordX, coordY);
                float distCentrCurrent = (Vector2.Distance(Centre, currentPos) < 10) ? 10 : Vector2.Distance(Centre, currentPos);
        //blending
                perlinValue = (distCentrCurrent < 50) ? 0.5f*2-1 : Mathf.PerlinNoise(sampleX, sampleY)*2-1;
        //perlinValue = ((0.5f*(10/ distCentrCurrent))/**Mathf.PerlinNoise(sampleX,sampleY)*Mathf.Clamp01(distCentrCurrent)*/) * 2- 1;
        //Debug.Log(perlinValue);
                //62 = milieux
        //Debug.Log(39 / Mathf.Abs(Vector2.Distance(Centre, currentPos)));
>>>>>>> Stashed changes
            }

        }
        //if (sampleCentre.x + sampleCentre.y == 125)
        //{
        //    Debug.Log(mapWidth);
        //    Debug.Log(mapHeight);
        //}

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
