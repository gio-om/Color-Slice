using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInBandMovement : MonoBehaviour
{
    private BandGenerator generator;
    private CubeToCanvasMovement movement;
    [HideInInspector]
    public Material mat;
    private GameManager gameManager;

    private void Awake()
    {
        mat = GetComponent<Renderer>().materials[0];
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        generator = transform.parent.GetComponent<BandGenerator>();
    }

    private void Update()
    {
        Vector3 changed = (generator.endPoint.localPosition - generator.startPoint.localPosition).normalized * generator.currentSpeed * Time.deltaTime;
        transform.Translate(changed);
        if ((transform.localPosition - generator.startPoint.localPosition).sqrMagnitude >= (generator.endPoint.localPosition - generator.startPoint.localPosition).sqrMagnitude)
        {
            gameObject.SetActive(false);
            if (mat.color == gameManager.CurrentCube.normal)
            {
                generator.Skip();
            }
        }
    }

    private void OnTriggerEnter(Collider other)

    {

        if (other.tag == "Axe")

        {

            generator.Hit();

            gameManager.CubeToCanvas(this);

        }

    }
}
