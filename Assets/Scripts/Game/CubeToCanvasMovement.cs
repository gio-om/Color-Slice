using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeToCanvasMovement : MonoBehaviour
{
    private bool ShouldMoveToPosition = false;

    public float Speed = 5f;
    [HideInInspector]
    public bool statement;  // when should we move our cube to place
    [HideInInspector]
    public CubeInCanvas place;  // place, where should we move our cube

    private GameManager gameManager;
    private BandGenerator bandGenerator;

    private void Start()
    {
        gameManager = GameManager.Instance;
        bandGenerator = BandGenerator.Instance;
    }

    private void Update()
    {
        if (statement)
        {
            ShouldMoveToPosition = true;
        }

        if (ShouldMoveToPosition)
        {
            transform.Translate((place.transform.position - this.transform.position).normalized * Speed * Time.deltaTime);
        }

        if (ShouldMoveToPosition && Vector3.Distance(this.transform.position, place.transform.position) < 0.05f)
        {
            ShouldMoveToPosition = false;
            this.gameObject.SetActive(false);
            place.PutCubeBand();
            //bandGenerator.isDeactivated = true;
        }
    }
}
