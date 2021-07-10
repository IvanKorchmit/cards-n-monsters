using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class isAzertyText : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void Toggle()
    {
        Settings.isAzerty = !Settings.isAzerty;
        if(Settings.isAzerty)
        {
            text.text = "Keyboard Mode: AZERTY";
        }
        else
        {
            text.text = "Keyboard Mode: QWERTY";
        }
    }
}
