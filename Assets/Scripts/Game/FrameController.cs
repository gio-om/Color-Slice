using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : Manager<FrameController>
{
    private GridController gridController;
    private LineRenderer line;
    public float height = 0.6f;
    public float lengthSide = 0.5f;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        gridController = GridController.Instance;
    }

    public void SetFrame(GameManager.Frame display, Vector2Int posCube)
    {
        if (display == GameManager.Frame.Horizontal)
        {
            //gridController.Cubes[posCube.y, 0].transform.position
            Vector3 left = gridController.Cubes[0, posCube.y].transform.position;
            Vector3 right = gridController.Cubes[gridController.ArtWidth - 1, posCube.y].transform.position;
            line.positionCount = 4;
            line.SetPositions(new Vector3[] {
                new Vector3(left.x - lengthSide, height, left.z + lengthSide),
                new Vector3(left.x - lengthSide, height, left.z - lengthSide),
                new Vector3(right.x + lengthSide, height, right.z - lengthSide),
                new Vector3(right.x + lengthSide, height, right.z + lengthSide)
            });
        }
        else if (display == GameManager.Frame.Vertical)
        {
            Vector3 down = gridController.Cubes[posCube.x, 0].transform.position;
            Vector3 up = gridController.Cubes[posCube.x, gridController.ArtHeight - 1].transform.position;
            line.SetPositions(new Vector3[] {
                new Vector3(down.x - lengthSide, height, down.z - lengthSide),
                new Vector3(down.x + lengthSide, height, down.z - lengthSide),
                new Vector3(up.x + lengthSide, height, up.z + lengthSide),
                new Vector3(up.x - lengthSide, height, up.z + lengthSide)
            });
        }
    }

    public void ClearLine()
    {
        line.positionCount = 0;
        line.SetPositions(new Vector3[0]);
    }
}
