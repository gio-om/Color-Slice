using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Manager<LevelManager>
{
    public string Scene = "ForMarat";
    [HideInInspector] public int Level = 0;
    [HideInInspector] public List<InGameImageLoader.Picture> remainingPictures;
    [HideInInspector] public InGameImageLoader.Picture currentPicture;
    private InGameImageLoader imageLoader;

    private void Awake()
    {
        imageLoader = InGameImageLoader.Instance;
        DontDestroyOnLoad(gameObject);
        remainingPictures = new List<InGameImageLoader.Picture>(imageLoader.PixArts);
        currentPicture = remainingPictures[Random.Range(0, remainingPictures.Count)];
    }

    public void NextLevel()
    {
        Level += 1;
        remainingPictures.Remove(currentPicture);
        if (remainingPictures.Count == 0)
        {
            remainingPictures = new List<InGameImageLoader.Picture>(imageLoader.PixArts);
        }
        currentPicture = remainingPictures[Random.Range(0, remainingPictures.Count)];
        SceneManager.LoadScene(Scene);
    }

    
}
