using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridLayoutScaler : MonoBehaviour
{
    public Canvas canvas;
    private GridLayoutGroup grid;
    private Vector2 cellSize;
    private Vector2 spacing;
    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        cellSize = grid.cellSize;
        spacing = grid.spacing;
    }
    private void OnGUI()
    {
        float f = canvas.scaleFactor;
        grid.cellSize = cellSize / f;
        grid.spacing = spacing / f;
    }
}
