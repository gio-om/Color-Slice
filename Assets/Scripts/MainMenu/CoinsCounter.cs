using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    private TextMeshProUGUI textField;

    private void Start()
    {
        textField = this.GetComponent<TextMeshProUGUI>();
        ChangeText();
    }

    private void ChangeText()
    {
        textField.text = PlayerPrefs.GetInt("CoinsAmount").ToString();
    }
}
