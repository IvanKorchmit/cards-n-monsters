using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FontSizeScaler : MonoBehaviour
{
    private TextMeshProUGUI text;
    public Canvas canvas;
    private float initFontSize;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        initFontSize = text.fontSize;

    }
    private void OnGUI()
    {
        text.fontSize = initFontSize * canvas.scaleFactor;
    }
}
