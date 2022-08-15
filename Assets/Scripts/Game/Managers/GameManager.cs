using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    public CubeInCanvas CurrentCube;
    [HideInInspector] public int CubesPainted;
    [HideInInspector] public bool IsHitbutton = false;
    public GameObject smallHam;
    public float MissDelay = 1f;
    public GameObject HitButton;
    public GameObject CongratsPanel;
    private int remaining = 0;

    private GridController gridController;
    private CameraController cam;
    private BandGenerator bandGenerator;
    private CoinsManager coinsManager;
    private AudioController audioController;
    private InGameImageLoader imageLoader;
    private PixelArts pixelArts;

    private Color[] colors;
    public Mode ModeCondition;
    // показывает, какая рамка отображается на данный момент
    private Frame frameCondition = Frame.Horizontal;
    private FrameController frame;

    public enum Mode
    {
        Canvas,
        Band
    }

    public enum Frame
    {
        Horizontal,
        Vertical
    }

    private void Awake()
    {
        audioController = AudioController.Instance;
        coinsManager = CoinsManager.Instance;
        frame = FrameController.Instance;
        bandGenerator = BandGenerator.Instance;
        gridController = GridController.Instance;
        cam = Camera.main.GetComponent<CameraController>();
        imageLoader = InGameImageLoader.Instance;
    }

    private void Start()
    {
        ModeCondition = Mode.Canvas;
    }

    public void StartBandMode()
    {
        if (ModeCondition != Mode.Band && CurrentCube != null)
        {
            ModeCondition = Mode.Band;
            cam.ViewSwitch = false;
            CurrentCube = gridController.Cubes[CurrentCube.PosInCanvas.x, CurrentCube.PosInCanvas.y];
            StartCoroutine(bandGenerator.StartGeneration());
            smallHam.SetActive(true);
        }
    }

    public void SetNextCube()
    {
        remaining += 1;
        CubesPainted += 1;
        if(CubesPainted == gridController.SquareOfArt)  // if the art if completly painted over 
                    {
                        pixelArts.Pushback(imageLoader.PixArts[0].Texture);     // adds completed picture to array
                        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                    }
        if (frameCondition == Frame.Horizontal)
        {
            Vector2Int posCube = CurrentCube.PosInCanvas;

            if (posCube.x != gridController.ArtWidth - 1)
            {
                CurrentCube = gridController.Cubes[posCube.x + 1, posCube.y];
                posCube = CurrentCube.PosInCanvas;
                if (!CurrentCube.isFree)
                {
                    if (CurrentCube.PosInCanvas.y == 0)
                        StartCanvasMode();
                    else
                    {
                        CurrentCube = gridController.Cubes[0, CurrentCube.PosInCanvas.y - 1];
                        while (!CurrentCube.isFree && CurrentCube.PosInCanvas.x != posCube.x)
                            CurrentCube = gridController.Cubes[CurrentCube.PosInCanvas.x + 1, CurrentCube.PosInCanvas.y];
                        if (CurrentCube.PosInCanvas.x == posCube.x)
                        {
                            StartCanvasMode();
                        }
                    }
                }
                if (CurrentCube.PosInCanvas.y == 0)
                    StartCanvasMode();
                else
                    SetFrame();
            }
            else  // if the row ends
            {
                CurrentCube = gridController.Cubes[0, CurrentCube.PosInCanvas.y - 1];
                while (!CurrentCube.isFree && CurrentCube.PosInCanvas.x != posCube.x)
                    CurrentCube = gridController.Cubes[CurrentCube.PosInCanvas.x + 1, CurrentCube.PosInCanvas.y];
                if (CurrentCube.PosInCanvas.x == posCube.x)
                {
                    StartCanvasMode();
                }
            }
        }
        else
        {
            Vector2Int posCube = CurrentCube.PosInCanvas;

            if (posCube.x != 0)
            {
                CurrentCube = gridController.Cubes[posCube.x, posCube.y + 1];
                posCube = CurrentCube.PosInCanvas;
                if (!CurrentCube.isFree)
                {
                    if (CurrentCube.PosInCanvas.x == gridController.ArtWidth - 1)
                        StartCanvasMode();
                    else
                    {
                        CurrentCube = gridController.Cubes[CurrentCube.PosInCanvas.x + 1, 0];
                        while (!CurrentCube.isFree && CurrentCube.PosInCanvas.y != posCube.y)
                            CurrentCube = gridController.Cubes[CurrentCube.PosInCanvas.x, CurrentCube.PosInCanvas.y + 1];
                        if (CurrentCube.PosInCanvas.y == posCube.y)
                        {
                            StartCanvasMode();
                        }
                    }
                }
                if (CurrentCube.PosInCanvas.y == gridController.ArtWidth - 1)
                    StartCanvasMode();
                else
                    SetFrame();
            }
        }
    }

    public void SetFrame()
    {
        frame.SetFrame(frameCondition, CurrentCube.PosInCanvas);
        gridController.SetFrame(frameCondition, CurrentCube.PosInCanvas);
    }

    public void PressCube(CubeInCanvas pressed)
    {
        if (CurrentCube == pressed)
        {
            if (frameCondition != Frame.Vertical)
                frameCondition = (Frame)((int)frameCondition + 1);
            else
                frameCondition = Frame.Horizontal;
        }
        else
        {
            CurrentCube = pressed;
        }
        SetFrame();
    }

    public void CubeToCanvas(CubeInBandMovement other)
    {
        if (IsHitbutton)
        {
            IsHitbutton = false;
            Color colOther = other.mat.color;
            Color colCur = CurrentCube.mat.color;
            other.enabled = false;
            other.transform.parent = null;
            CubeToCanvasMovement cube = other.GetComponent<CubeToCanvasMovement>();
            GameObject particle = cube.transform.GetChild(0).gameObject;
            particle.GetComponent<ParticleSystem>().GetComponent<Renderer>().material.color = colCur;
            particle.SetActive(true);
            //cube.GetComponentInChildren<ParticleSystem>().gameObject.SetActive(true);
            if (colOther.r == colCur.r && colOther.g == colCur.g && colOther.b == colCur.b)
            {
                audioController.PlayRighSound();
                cube.statement = true;
                cube.place = CurrentCube;
                SetNextCube();
            }
            else
            {
                audioController.PlayWrongSound();
                coinsManager.ClearStreak();
                StartCoroutine(MissCoolDown());
                cube.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator MissCoolDown()
    {
        HitButton.SetActive(false);
        bandGenerator.Miss();
        yield return new WaitForSeconds(MissDelay);
        HitButton.SetActive(true);
    }

    public void StartCanvasMode()
    {
        CurrentCube = null;
        frame.ClearLine();
        ModeCondition = Mode.Canvas;
        cam.ViewSwitch = !cam.ViewSwitch;
        bandGenerator.started = false;
        HammerHit ham = smallHam.GetComponent<HammerHit>();
        smallHam.transform.localPosition = ham.pos;
        smallHam.transform.localRotation = ham.rot;
        smallHam.SetActive(false);
    }
}
