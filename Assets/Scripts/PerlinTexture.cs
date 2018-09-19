using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinTexture
{

    public static Texture2D CalcNoise(Texture2D noiseTex, int pixHeight, int pixWidth, float xOrg, float yOrg, float scale, float amplitude)
    {
        // For each pixel in the texture...
        Color[] pix;
        pix = new Color[noiseTex.width * noiseTex.height];



        float y = 0.0F;




        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
                pix[(int)y * noiseTex.width + (int)x] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();

        return noiseTex;
    }


    public static IEnumerable<Vector3[]> CoColorVector(int pixHeight, int pixWidth, Vector3[] rgb, float xOrg, float yOrg, float scale, float amplitude)
    {
         
        float y = 0.0F;

        while (y < pixHeight)
        {
            float x = 0.0F;
            while (x < pixWidth)
            {
                float xCoord = xOrg + x / pixWidth * scale;
                float yCoord = yOrg + y / pixHeight * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
                rgb[(int)y * pixWidth + (int)x] = new Vector3(sample, sample, sample);
                x++;
            }
            y++;
            yield return rgb;
        }

    }

    public static Vector3[] ColorVector(int pixHeight, int pixWidth, Vector3[] rgb, float xOrg, float yOrg, float scale, float amplitude)
    {

        float y = 0.0F;

        while (y < pixHeight)
        {
            float x = 0.0F;
            while (x < pixWidth)
            {
                float xCoord = xOrg + x / pixWidth * scale;
                float yCoord = yOrg + y / pixHeight * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
                rgb[(int)y * pixWidth + (int)x] = new Vector3(sample, sample, sample);
                x++;
            }
            y++;
            
        }
        return rgb;
    }

    public static float VectorToGreyScale(Vector3 rgbVect)
    {

        return (rgbVect.x + rgbVect.y + rgbVect.z) / 3;

    }

    public static float VectorToGreyScale(IEnumerator<Vector3> rgbVect)
    {

        return (rgbVect.Current.x + rgbVect.Current.y + rgbVect.Current.z) / 3;

    }

}
