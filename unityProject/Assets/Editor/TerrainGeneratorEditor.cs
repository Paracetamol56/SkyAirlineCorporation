using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{

  public override void OnInspectorGUI()
  {
    TerrainGenerator generator = (TerrainGenerator)target;

    if (DrawDefaultInspector())
    {

    }
    if (GUILayout.Button("Test"))
    {
      generator.BlackBox(3000, -3000, new Vector2(0, 0));
    }
  }
}
