using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    public float ZoomSpeed;
    private Camera mainCamera;

    private GameManager gameManager;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if(gameManager.ModeCondition == GameManager.Mode.Canvas)
        {
            if (Input.touchCount == 2)  // Zoom
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitude = prevTouchDeltaMag - touchDeltaMag;

                mainCamera.fieldOfView += deltaMagnitude * ZoomSpeed;
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, .1f, 179.9f);
            }


        }
    }
}
