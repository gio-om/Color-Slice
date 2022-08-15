using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameImageLoader : Manager<InGameImageLoader>
{
    public Picture[] PixArts;
    
    [System.Serializable]
    public struct Picture
    {
        public string name;
        public Texture2D Texture;
        public int RowSize;
        public int ColumnSize;
        public int OffsetX;
        public int OffsetY;
        public int PaddingX;
        public int PaddingY;
    }

    public float MaxDifference = 0.15f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Color[,] CreatePicture(int NumberOfPicture)
    {
        Picture PixArt = PixArts[NumberOfPicture];
        int CurrentRowSize = PixArt.RowSize;
        int CurrentColumnSize = PixArt.ColumnSize;
        List<Color> NormalizedColors = new List<Color>();
        Color[,] NotNormalizedColors = AnalysePixel.Analyse(PixArt.Texture, CurrentRowSize, CurrentColumnSize, PixArt.OffsetX, PixArt.OffsetY, PixArt.PaddingX, PixArt.PaddingY);

        for(int row = 0; row < CurrentRowSize; row++)
        {
            for(int column = 0; column < CurrentColumnSize; column++)
            {
                // Check is it new color or not
                bool foundBaseColor = false;
                Color CurrentColor = NotNormalizedColors[row, column];
                Color BaseColorFound = CurrentColor;
                foreach(Color BaseColor in NormalizedColors)
                {
                    if(CalculateDistanceBetweenColors(BaseColor, CurrentColor) < MaxDifference)
                    {
                        foundBaseColor = true;
                        BaseColorFound = BaseColor;
                        break;
                    }
                }
                if(foundBaseColor)
                {
                    NotNormalizedColors[row, column] = BaseColorFound;
                }
                else
                {
                    NormalizedColors.Add(CurrentColor);
                    
                }
            }
        }
        GridController.Instance.AllColorsInCanvas = NormalizedColors.ToArray();
        return NotNormalizedColors;
    }

    public static float CalculateDistanceBetweenColors(Color BaseColor, Color CompareColor)
    {
        float R = BaseColor.r - CompareColor.r;
        float G = BaseColor.g - CompareColor.g;
        float B = BaseColor.b - CompareColor.b;

        float D = (Mathf.Sqrt(R * R + G * G) + Mathf.Sqrt(G * G + B * B) + Mathf.Sqrt(B * B + R * R)) / 3;
        return D;
    }
}
