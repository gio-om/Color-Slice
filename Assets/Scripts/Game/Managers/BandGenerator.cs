using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandGenerator : Manager<BandGenerator>
{
    public GameObject PrefabCube;
    public float StartChanceKoef = 0.3f;
    public float Distance;
    public float startSpeed = 2f;
    public float speedGrow;
    public float SkipDelay = 1f;
    public float SkipDecrease;
    private Color color;
    private Color[] allColors;
    private float currentChance;
    private float cons;
    private float koef;
    [HideInInspector]
    public Transform startPoint;
    [HideInInspector]
    public Transform endPoint;
    //[HideInInspector] public bool isDeactivated = false;
    private Vector3 lastCubePos;
    private GameObject lastCube;
    private float currentDistance;
    public float currentSpeed;
    private bool isSkipDelay;
    public bool started;

    private Pooler pooler;
    private GameManager gameManager;
    private ColorIndicator colorIndicator;
    //private InGameImageLoader imageLoader;

    private void Awake()
    {
        currentSpeed = startSpeed;
        startPoint = transform.Find("StartPoint");
        endPoint = transform.Find("EndPoint");
    }

    private void Start()
    {
        allColors = GridController.Instance.AllColorsInCanvas;
        koef = StartChanceKoef * allColors.Length;
        currentChance = cons * koef;
        pooler = Pooler.instance;
        gameManager = GameManager.Instance;
    }

    public IEnumerator StartGeneration()
    {
        float chance = 100 / allColors.Length;
        cons = -0.0001f * Mathf.Pow(chance, 3) + 0.01f * Mathf.Pow(chance, 2) + 0.1483f * chance - 0.8432f;
        yield return new WaitForSeconds(1);
        started = true;
        while (started)
        {
            if (lastCube == null || currentDistance >= Distance)
            {
                lastCube = pooler.SpawnFromPull(PrefabCube, startPoint.position, transform);
                GameObject particle = lastCube.transform.GetChild(0).gameObject;
                particle.SetActive(false);
                lastCube.GetComponent<CubeToCanvasMovement>().statement = false;
                lastCube.GetComponent<CubeInBandMovement>().enabled = true;
                if (Random.Range(0, 100) < currentChance)
                {
                    lastCube.GetComponent<Renderer>().materials[0].color = gameManager.CurrentCube.normal;
                    currentChance = cons * koef;
                }
                else
                {
                    currentChance += cons;
                    Color color = allColors[Random.Range(0, allColors.Length)];
                    while (color == gameManager.CurrentCube.normal)
                        color = allColors[Random.Range(0, allColors.Length)];
                    lastCube.GetComponent<Renderer>().materials[0].color = color;
                }
                currentDistance = 0;
                lastCubePos = startPoint.localPosition;
            }
            else
            {
                currentDistance += (lastCube.transform.localPosition - lastCubePos).magnitude;
                lastCubePos = lastCube.transform.localPosition;
            }
            yield return null;
        }
    }

    public void Hit()
    {
        currentSpeed += speedGrow;
    }

    public void Miss()
    {
        currentSpeed = startSpeed;
    }

    public IEnumerator SkipDelayStart()
    {
        isSkipDelay = true;
        yield return new WaitForSeconds(SkipDelay);
        isSkipDelay = false;
    }

    public void Skip()
    { 
        if (!isSkipDelay)
        {
            StartCoroutine(SkipDelayStart());
            currentSpeed -= SkipDecrease;
            if (currentSpeed < startSpeed)
                currentSpeed = startSpeed;
        }
    }

    /*private void GetColorsFromImage()
    {
        imageLoader.CreatePicture(0);
        Colors = imageLoader.GetAllColors().ToArray();
    }*/
}
