using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public Sprite MusicOnImage;
    public Sprite MusicOffImage;
    public bool Sound = true;

    private Image imageLoader;
    
    private void Start()
    {
        imageLoader = this.GetComponent<Image>();
    }

    public void ChangeCondition()
    {
        if(Sound)
        {
            imageLoader.sprite = MusicOffImage;
            Sound = false;
        }
        else
        {
            imageLoader.sprite = MusicOnImage;
            Sound = true;
        }
    }
}
