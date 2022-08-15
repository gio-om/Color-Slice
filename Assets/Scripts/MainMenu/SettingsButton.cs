using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject LockMusicButton;
    public GameObject LockVibrationButton;

    public void ShowSettings()
    {
        LockMusicButton.SetActive(!LockMusicButton.activeSelf);
        LockVibrationButton.SetActive(!LockVibrationButton.activeSelf);
    }
}
