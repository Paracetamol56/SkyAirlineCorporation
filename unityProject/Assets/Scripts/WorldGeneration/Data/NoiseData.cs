using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class NoiseData : UpdatableData
{
    public float noiseScale;

    public int octavesnb;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public int seed;

    public Vector2 offset;

    public PerlinNoiseGenerator.NormalizeMode normalizeMode;
    #if UNITY_EDITOR
    protected override void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octavesnb < 0)
        {
            octavesnb = 0;
        }
        base.OnValidate();
    }
    #endif
}
