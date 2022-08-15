using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GridController : Manager<GridController>
{
    public InGameImageLoader.Picture picture;
    public Texture2D tex;
    public GameObject ObjectToSpawn;
    public int ArtWidth;
    public int ArtHeight;
    public float ArtpixelOffset;
    public float bright = 0.2f;
    public float CompositionKoef = 0.3f;
    public float MultyplierKoef = 0.3f;
    private Vector4[] areas;
    public int count1 = 3;
    public int count2 = 5;
    private int count;
    public int square1 = 40;
    public int square2 = 15;
    public int Indent = 3;
    private int square;

    [HideInInspector] public int SquareOfArt;
    public float transparency;
    [HideInInspector] public float scaleValue;
    [HideInInspector] public CubeInCanvas[,] Cubes;
    [HideInInspector] public Material[,] CubesMaterial;
    [HideInInspector] public Vector3 CenterCube;
    [HideInInspector] public Vector3 CamOffset;
    [HideInInspector] public float camXOffset;
    [HideInInspector] public float camYOffset;
    [HideInInspector] public float camZOffset;
    public Color[] AllColorsInCanvas;

    private CameraController cam;
    private GameManager gameManager;
    private InGameImageLoader imageLoader;
    private LevelManager levelManager;

    private CubeInCanvas[] coloredCubes;

    private void Awake()
    {
        coloredCubes = new CubeInCanvas[0];
        levelManager = LevelManager.Instance;
        imageLoader = InGameImageLoader.Instance;
        gameManager = GameManager.Instance;
        
        cam = Camera.main.GetComponent<CameraController>();
        picture = levelManager.currentPicture;
        SpawnGrid();
        SetPicture(levelManager.currentPicture);
    }

    //public void GetFree(Vector2Int pos, GameManager.Frame frame)
    //{
    //    if (frame == GameManager.Frame.Horizontal)
    //    {
    //        while (!Cubes[pos.x, pos.y].isFree)
    //            if (pos.x != ArtWidth - 1)
    //            {

    //            }
    //    }
    //}

    void SpawnGrid()
    {
        count = Random.Range(count1, count2);
        int square = Random.Range(square1, square2);
        int[,] sides = GetRandomAreas(count, square);
        areas = new Vector4[count];
        tex = picture.Texture;
        ArtWidth = picture.RowSize;
        ArtHeight = picture.ColumnSize;
        Cubes = new CubeInCanvas[ArtWidth, ArtHeight];
        CubesMaterial = new Material[ArtWidth, ArtHeight];
        for (int i = 0; i < count; i++)
            areas[i] = new Vector4(Random.Range(Indent, ArtWidth - Indent), Random.Range(Indent, ArtWidth - Indent), sides[i, 0], sides[i, 1]);
        for (int x = 0; x < ArtWidth; x++)
        {
            for (int z = 0; z < ArtHeight; z++)
            {
                Vector3 spawnPosition = new Vector3(x * ArtpixelOffset, 0, z * ArtpixelOffset);
                Cubes[x, z] = Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity).GetComponent<CubeInCanvas>();
                Cubes[x, z].PosInCanvas = new Vector2Int(x, z);
                CubesMaterial[x, z] = Cubes[x, z].mat;
            }
        }
        camXOffset = ArtWidth / 2 - 0.5f + (ArtWidth - 1) * (ArtpixelOffset - 1) / 2;
        camYOffset = ArtWidth / 0.5f;
        camZOffset = (ArtHeight / 2) * 0.5f;
        CamOffset = new Vector3(camXOffset, camYOffset, camZOffset);
        cam.defaultPos = CamOffset;
        CenterCube = Vector3.Lerp(Cubes[ArtWidth - 1, ArtHeight - 1].transform.position, Cubes[0, 0].transform.position, 0.5f);

    }

    private int[,] GetRandomAreas(int count, int square)
    {
        float[] multyples = new float[count];
        int[,] sides = new int[count, 2];
        float squareMore = square;
        for (int i = 0; i < count; i++)
        {
            multyples[i] = Random.Range(squareMore / (count - i) * (1 - CompositionKoef), squareMore / (count - i) * (1 + CompositionKoef));
            squareMore -= multyples[i];
        }
        for (int i = 0; i < count; i++)
        {
            float x = Mathf.Sqrt(multyples[i]);
            x = Random.Range(x * (1 - MultyplierKoef), x * (1 + MultyplierKoef));
            sides[i, 0] = (int)Mathf.Round(x);
            sides[i, 1] = (int)Mathf.Round(multyples[i] / x);
        }
        return sides;
    }

    public void SetFrame(GameManager.Frame frame, Vector2Int PosInCanvas)
    {
        for (int i = 0; i < coloredCubes.Length; i++)
        {
            coloredCubes[i].DeleteFromFrame();
        }
        if (frame == GameManager.Frame.Horizontal)
        {
            coloredCubes = new CubeInCanvas[ArtWidth];
            for (int i = 0; i < coloredCubes.Length; i++)
            {
                coloredCubes[i] = Cubes[i, PosInCanvas.y];
                coloredCubes[i].SetInFrame();
            }
        }
        else
        {
            coloredCubes = new CubeInCanvas[ArtHeight];
            for (int i = 0; i < coloredCubes.Length; i++)
            {
                coloredCubes[i] = Cubes[PosInCanvas.x, i];
                coloredCubes[i].SetInFrame();
            }
        }
    }

    private void SetPicture(InGameImageLoader.Picture picture)
    {
        SetPicture(System.Array.IndexOf(imageLoader.PixArts, picture));
    }

    private void SetPicture(int NumberPicture)
    {
        Color[,] colors = imageLoader.CreatePicture(NumberPicture);
        bool da = false;
        for (int i = 0; i < ArtWidth; i++)
            for (int j = 0; j < ArtHeight; j++)
            {

                da = false;
                for (int u = 0; u < count; u++)
                {
                    if ((areas[u].x <= i) && (i <= (areas[u].x + areas[u].z)) && (areas[u].y <= j) && j <= (areas[u].y + areas[u].w))
                    {
                        da = true;
                        break;
                    }
                }
                if (da)
                {
                    Debug.Log("asd");
                }
                Cubes[ArtWidth - i - 1, ArtHeight - j - 1].SetColor(colors[i, j], da);
            }
        SquareOfArt = ArtHeight * ArtWidth;
        gameManager.CubesPainted = 0;
    }

    public void GetColorsInRow(int row, out Color[] colors)
    {
        colors = new Color[ArtWidth];
        for (int i = 0; i < ArtWidth; i++)
        {
            colors[i] = CubesMaterial[i, row].color;
        }
    }

    public static T[] MakeSet<T>(T[] objects)
    {
        List<T> objs = new List<T>();
        for(int i = 0; i < objects.Length; i++)
        {
            if (!objs.Contains(objects[i]))
            {
                objs.Add(objects[i]);
            }
        }
        return objs.ToArray();
    } 
}


