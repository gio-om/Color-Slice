using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsButton : MonoBehaviour
{
    public string SkinsSceneName = "SkinsShop";

    public void LoadSkinsScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SkinsSceneName);
    }
}
