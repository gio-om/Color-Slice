using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : Manager<CollectionManager>
{
    public GameObject plane;         // place PlaneForCollection here
    public float PictureOffset = 4f; // distance between each picture
    public float ScaleValue = 0.01f; // scales plane from (width, height) to (width * scalevalue, height * scalevalue)

    private PixelArts pixelArts;
    private void Awake()
    {
        for(int i = 0; i < pixelArts.PixArts.Length; i++)   // instantiate a plane with picture texture and set localscale to fit texture in plane
        {
            GameObject clone = Instantiate(plane); 
            clone.GetComponent<Renderer>().material.SetTexture("_MainTex", pixelArts.PixArts[i]);
            clone.transform.position = Camera.main.transform.position + new Vector3(i * PictureOffset, 0f, 0f);
            clone.transform.localScale = new Vector3(pixelArts.PixArts[i].width * ScaleValue, pixelArts.PixArts[i].height * ScaleValue, 1f);
        }
    }
}
