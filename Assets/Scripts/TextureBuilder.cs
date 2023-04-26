using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureBuilder
{
    // build a texture based on the noise map
    public static Texture2D BuildTexture(float[,] noiseMap, TerrainType[] terrainTypes)
    {
        // create colore array for the pixels
        Color[] pixels = new Color[noiseMap.Length];
        
        // calculate the length of the texture
        int pixelLength = noiseMap.GetLength(0);

        // loop trough each pixel and set the color based on the noise map
        for(int x= 0; x < pixelLength; x++)
        {
            for( int z = 0; z < pixelLength; z++)
            {
                // calculate the index of the pixel in the array 'pixels'
                int index = (x * pixelLength) + z;
                //pixels[index] = Color.Lerp(Color.black, Color.white, noiseMap[x, z]);

                for(int t = 0; t < terrainTypes.Length; t++)
                {
                    if (noiseMap[x, z] < terrainTypes[t].threshold)
                    {
                        float minVal = t == 0 ? 0 : terrainTypes[t - 1].threshold;
                        float maxVal = terrainTypes[t].threshold;

                        pixels[index] = terrainTypes[t].colorGradient.Evaluate(1.0f - (maxVal - noiseMap[x, z]) / (maxVal - minVal));
                        break;
                    }
                }
            }
        }

        // create a new texture and set the pixels
        Texture2D texture = new Texture2D(pixelLength, pixelLength);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Bilinear;
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }

    public static TerrainType[,] CreateTerrainTypeMap(float[,] noiseMap, TerrainType[] terrainTypes)
    {
        int size = noiseMap.GetLength(0);
        TerrainType[,] outputMap = new TerrainType[size, size];

        for(int x = 0; x < size; x++)
        {
            for(int z = 0; z < size; z++)
            {
                for(int t = 0; t < terrainTypes.Length; t++)
                {
                    if (noiseMap[x, z] < terrainTypes[t].threshold)
                    {
                        outputMap[x, z] = terrainTypes[t];
                        break;
                    }
                }
            }
        }

        return outputMap;
    }
}
