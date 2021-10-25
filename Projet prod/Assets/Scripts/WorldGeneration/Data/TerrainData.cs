using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class TerrainData : UpdatableData
{
    public float uniformScale = 10f;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool useFlatShading;

}
