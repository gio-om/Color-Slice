using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchView : Manager<SwitchView>
{
    private GameManager gameManager;
    private CameraController cameraController;
    
    public GameObject bandGenerator;
    public GameObject smallHammer;
    public GameObject hitButton;

    void Start()
    {
        cameraController = CameraController.Instance;
        gameManager = GameManager.Instance;
    }

    public void Switch()
    {
        if (gameManager.ModeCondition == GameManager.Mode.Band)
        {
            gameManager.StartCanvasMode();
        }
    }
}
