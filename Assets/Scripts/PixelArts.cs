using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;

public class PixelArts
{
    public Texture2D[] PixArts;     // array that we should "give" in collection scene
    
    public void Pushback(Texture2D texture)     // function that adds a picture to pixarts array
    {
        Texture2D[] temp = new Texture2D[PixArts.Length + 1];
        for(int i = 0; i < PixArts.Length; i++)
        {
            temp[i] = PixArts[i];
        }
        temp[PixArts.Length - 1] = texture;
        PixArts = new Texture2D[temp.Length];
        PixArts = temp;
    }
}
