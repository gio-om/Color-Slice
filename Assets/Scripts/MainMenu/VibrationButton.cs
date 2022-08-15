using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationButton : MonoBehaviour
{
    public Sprite VibrationOnImage;
    public Sprite VibrationOffImage;
    public bool Vibration = true;

    private Image imageLoader;

    private void Start()
    {
        imageLoader = this.GetComponent<Image>();
    }

    public void ChangeCondition()
    {
        if (Vibration)
        {
            imageLoader.sprite = VibrationOffImage;
            Vibration = false;
        }
        else
        {
            imageLoader.sprite = VibrationOnImage;
            Vibration = true;
        }
    }
}
