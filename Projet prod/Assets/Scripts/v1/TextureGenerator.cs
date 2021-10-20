using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D TextureFromCoulourMap(Color[] colourMap,int largeur, int hauteur)
    {
        Texture2D texture = new Texture2D(largeur, hauteur);
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }
    public static Texture2D TextureFromHeightMap(float[,] heightmap)
    {
        int largeur = heightmap.GetLength(0);
        int hauteur = heightmap.GetLength(1);

        Color[] colorMap = new Color[largeur * hauteur];
        for (int y = 0; y < hauteur; y++)
        {
            for (int x = 0; x < largeur; x++)
            {
                colorMap[y * largeur + x] = Color.Lerp(Color.black, Color.white, heightmap[x, y]);
                //Debug.Log(noiseMap[x, y]);
            }
        }
        return TextureFromCoulourMap(colorMap,largeur,hauteur);
    }
}
