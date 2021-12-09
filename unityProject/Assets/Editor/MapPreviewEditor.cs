using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapPreview))]
public class MapPreviewEditor : Editor
{

	public override void OnInspectorGUI()
	{
		MapPreview mapPreview = (MapPreview)target;
		TerrainGenerator terrain = (TerrainGenerator)target;
		if (DrawDefaultInspector())
		{
			if (mapPreview.autoUpdate)
			{
				mapPreview.DrawMapInEditor();
			}
		}

		if (GUILayout.Button("Generate"))
		{
			mapPreview.DrawMapInEditor();
		}
		if (GUILayout.Button("Test"))
        {
			terrain.BlackBox(40,60,new Vector2(0,0));
        }
	}
}