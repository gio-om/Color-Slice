using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public string GameSceneName = "DoNotTouch";
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameSceneName);
    }
}
