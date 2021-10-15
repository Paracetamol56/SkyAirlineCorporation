using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public void DrawNoiseMap(float[,] noiseMap)
    {
        int largeur = noiseMap.GetLength(0);
        int hauteur = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(largeur, hauteur);

        Color[] colorMap = new Color[largeur * hauteur];
        for(int y = 0; y < hauteur; y++)
        {
            for(int x = 0; x < largeur; x++)
            {
                colorMap[y * largeur + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
                //Debug.Log(noiseMap[x, y]);
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(largeur, 1, hauteur);
    }
}
