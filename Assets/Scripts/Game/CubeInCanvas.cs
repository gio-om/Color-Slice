using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInCanvas : MonoBehaviour
{
    //Это не координата в пространстве, а координата в холсте
    [HideInInspector]public Vector2Int PosInCanvas;
    [HideInInspector] public Material mat;
    private GameManager gameManager;
    public bool isFree = true;
    [HideInInspector] public Color blackWhite;
    private Color normalClarity;
    [HideInInspector] public Color normal;

    private GridController gridController;

    private void Awake()
    {
        gridController = GridController.Instance;
        mat = GetComponent<Renderer>().materials[0];
        gameManager = GameManager.Instance;
    }

    public void PutCubeBand()
    {
        mat.color = normal;
        isFree = false;
    }

    private void OnMouseDown()
    {
        if (gameManager.ModeCondition == GameManager.Mode.Canvas)
            gameManager.PressCube(this);

    }

    public void SetColor(Color color, bool isFree)
    {
        this.isFree = isFree;
        normal = color;
        normalClarity = new Color(color.r, color.g, color.b, gridController.transparency);
        float blAndWh = (color.r + color.g + color.b) / 3 + gridController.bright;
        blackWhite = new Color(blAndWh, blAndWh, blAndWh, gridController.transparency);
        if (isFree)
            mat.color = blackWhite;
        else
            mat.color = normal;
    }

    public void SetInFrame()
    {
        if (isFree)
            mat.color = normalClarity;
    }

    public void DeleteFromFrame()
    {
        if (mat.color != normal)
            mat.color = blackWhite;
    }
}
