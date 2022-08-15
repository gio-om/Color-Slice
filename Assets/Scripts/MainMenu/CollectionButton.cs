using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionButton : MonoBehaviour
{
    public string CollectionSceneName = "Collection";

    public void LoadCollectionScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(CollectionSceneName);
    }
}
