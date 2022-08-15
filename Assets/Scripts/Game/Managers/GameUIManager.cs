using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : Manager<GameUIManager>
{
    public GameObject PauseMenu;
    [HideInInspector] public bool IsButton = false;
    [HideInInspector] public bool IsPaused = false;

    void Update()
    {
        if (IsButton)
        {
            IsPaused = !IsPaused;
            PauseMenu.SetActive(IsPaused);
            Time.timeScale = IsPaused ? 0 : 1;
            IsButton = false;
        }
    }

    public void OnClick()
    {
        IsButton = true;
    }
}
