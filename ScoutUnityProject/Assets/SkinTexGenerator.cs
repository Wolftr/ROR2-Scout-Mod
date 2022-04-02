using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkinTexGenerator : MonoBehaviour
{
    public Texture2D source;

    public Texture2D result;

    void Start()
    {
        result = DuplicateTexture(source);
        result.name = source.name;

		for (int x = 0; x < result.width; x++)
		{
            for (int y = 0; y < result.height; y++)
            {
                if (result.GetPixel(x, y).a == 0)
                {
                    result.SetPixel(x, y, GetColorFromSurrounding(x, y));
                }
            }
        }

        result.Apply();

        byte[] bytes = result.EncodeToPNG();
        var dirPath = Application.streamingAssetsPath;
        File.WriteAllBytes(dirPath + "Result" + ".png", bytes);
    }

    Color GetColorFromSurrounding(int x, int y)
	{
        Color north;
        Color east;
        Color west;
        Color south;

        int numColors = 0;

        // Get surrounding colors
        if (!(y + 1 >= result.height))
		{
            Color pixel = result.GetPixel(x, y + 1);

            if (pixel.a == 0)
                north = Color.black;
			else
			{
                north = pixel;
                numColors++;
            }
        }
        else
            north = Color.black;

        if (!(x + 1 >= result.width))
		{
            Color pixel = result.GetPixel(x + 1, y);

            if (pixel.a == 0)
                east = Color.black;
            else
            {
                east = pixel;
                numColors++;
            }
        }
        else
            east = Color.black;

        if (!(x - 1 < 0))
		{
            Color pixel = result.GetPixel(x - 1, y);

			if (pixel.a == 0)
				west = Color.black;
			else
			{
				west = pixel;
				numColors++;
			}
		}
        else
            west = Color.black;

        if (!(y - 1 < 0))
		{
            Color pixel = result.GetPixel(x, y - 1);

			if (pixel.a == 0)
				south = Color.black;
			else
			{
				south = pixel;
				numColors++;
			}
		}
        else
            south = Color.black;
        
        // Get averaged color
        return AverageColorValue(new Color[4] { north, east, west, south }, numColors);
    }

    Color AverageColorValue(Color[] colors, int numColors)
	{
        Color averageColor = Color.black;

        foreach (Color color in colors)
        {
            averageColor.r += color.r;
            averageColor.g += color.g;
            averageColor.b += color.b;
        }

        averageColor = new Color(averageColor.r / numColors, averageColor.g / numColors, averageColor.b / numColors);

        return averageColor;
    }

    Texture2D DuplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}
