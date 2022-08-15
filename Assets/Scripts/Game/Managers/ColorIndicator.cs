using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ColorIndicator : MonoBehaviour
{
    public UnityEngine.UI.Image ThisColor; 
    void Start()
    {
        ThisColor = this.GetComponent<UnityEngine.UI.Image>();
    }
}
