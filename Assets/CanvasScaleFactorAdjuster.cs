using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
public class CanvasScaleFactorAdjuster : MonoBehaviour
{
    public PixelPerfectCamera MainCamera;
    public CanvasScaler canvasScale;

    void Start()
    {
        MainCamera = Camera.main.GetComponent<PixelPerfectCamera>();
        canvasScale = GetComponent<CanvasScaler>();
        AdjustScalingFactor();
    }

    void LateUpdate()
    {
        AdjustScalingFactor();
    }

    void AdjustScalingFactor()
    {
        canvasScale.scaleFactor = MainCamera.pixelRatio;
    }

}
