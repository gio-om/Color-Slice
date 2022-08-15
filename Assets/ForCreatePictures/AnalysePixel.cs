using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalysePixel
{
    static public Color[,] Analyse(Texture2D texture, int numberOfArtpixelsInRow, int numberOfArtpixelsInColumn, int offsetX, int offsetY, int paddingX, int paddingY)
    {
        Color[,] allColors = new Color[texture.width, texture.height];
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                Color colos = texture.GetPixel(i, texture.height - j);
                allColors[i, j] = texture.GetPixel(texture.width - i, texture.height - j);
            }
        }
        int width_sec = (texture.width - offsetX - paddingX * (numberOfArtpixelsInRow - 1)) / numberOfArtpixelsInRow;
        int height_sec = (texture.height - offsetY - paddingY * (numberOfArtpixelsInColumn - 1)) / numberOfArtpixelsInColumn;
        Sector[,] sectors = new Sector[numberOfArtpixelsInRow, numberOfArtpixelsInColumn];
        for (int i = 0; i < numberOfArtpixelsInRow; i++)
        {
            for (int j = 0; j < numberOfArtpixelsInColumn; j++)
            {
                sectors[i, j] = new Sector(offsetX + (height_sec + paddingY) * i, offsetY + (width_sec + paddingX) * j, width_sec, height_sec);
            }
        }

        Color[,] colors = new Color[numberOfArtpixelsInRow, numberOfArtpixelsInColumn];
        for (int i = 0; i < numberOfArtpixelsInRow; i++)
        {
            for (int j = 0; j < numberOfArtpixelsInColumn; j++)
            {
                int[] coord = sectors[i, j].GetCenter();
                Color color = allColors[coord[0], coord[1]];
                colors[i, j] = color;
            }
        }
        return colors;
    }

    public struct Sector
    {
        public int x, y, w, h;

        public Sector(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public int[] GetCenter()
        {
            return new int[] { x + w / 2, y + h / 2 };
        }
    }
}
