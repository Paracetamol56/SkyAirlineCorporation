using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnGenerator
{
    public static float GenerateSpawnMap(Vector2 sampleCentre,int coordX,int coordY,float sampleX,float sampleY)
    {
        float perlinValue;
        Vector2 Centre = new Vector2(62, 62);
        Vector2 currentPos = new Vector2(coordX+sampleCentre.x, coordY+sampleCentre.y);
        float distCentrCurrent = (Vector2.Distance(Centre, currentPos) < 10) ? 10 : Vector2.Distance(Centre, currentPos);
        float blendValue=1;
        //Debug.Log(sampleCentre);
        //int x;
        //int y;
        if (Mathf.Abs(coordY) +Mathf.Abs(sampleCentre.y)<110&&Mathf.Abs(sampleCentre.x)+Mathf.Abs(coordX) <110)
        {
            if (Mathf.Abs(coordY) + Mathf.Abs(sampleCentre.y) > 12 && Mathf.Abs(sampleCentre.x) + Mathf.Abs(coordX) > 12)
            {
                //perlinValue = 0.5f * 2 - 1;

                
        //blending
                
                
        //perlinValue = ((0.5f*(10/ distCentrCurrent))/**Mathf.PerlinNoise(sampleX,sampleY)*Mathf.Clamp01(distCentrCurrent)*/) * 2- 1;
        //Debug.Log(perlinValue);
                //62 = milieux
        //Debug.Log(39 / Mathf.Abs(Vector2.Distance(Centre, currentPos)));
            }

        }
        if (Mathf.Abs(Vector2.Distance(Centre, currentPos)) > 70.0f)
        {
            blendValue = 1;
        }
        else
        {
            blendValue = 50.0f / Mathf.Abs(Vector2.Distance(Centre, currentPos));
        }
        //Debug.Log(Evaluate(1f));
        perlinValue = (distCentrCurrent < 60) ? 0 : ((1 - blendValue) * (-0.001f)) + ((Mathf.PerlinNoise(sampleX, sampleY)*2- 1) * blendValue);
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
