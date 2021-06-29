using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopySizeUI : MonoBehaviour
{
    public RectTransform CopyFrom;
    private RectTransform currentRect;
    public bool isWidth;

    private void Start()
    {
        currentRect = GetComponent<RectTransform>();
    }

    private void OnGUI()
    {
        if (CopyFrom != null)
        {
            if (isWidth)
            {
                currentRect.sizeDelta = new Vector2(CopyFrom.rect.width, currentRect.sizeDelta.y);
            }
            else
            {
                currentRect.sizeDelta = new Vector2(currentRect.sizeDelta.x, CopyFrom.rect.height);
            }
        }
    }
}
