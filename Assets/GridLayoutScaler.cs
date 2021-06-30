using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridLayoutScaler : MonoBehaviour
{
    public Canvas canvas;
    public int coefficient;
    private GridLayoutGroup grid;
    private Vector2 cellSize;
    private Vector2 spacing;
    private RectOffset padding;
    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        cellSize = grid.cellSize;
        spacing = grid.spacing;
        padding = grid.padding;
    }
    private void OnGUI()
    {
        float factor = canvas.scaleFactor;

        grid.cellSize = cellSize / factor;
        grid.spacing = spacing / factor;
        grid.padding = new RectOffset((padding.left / (int)factor) * coefficient, (padding.right / (int)factor) * coefficient, (padding.top / (int)factor) * coefficient, (padding.bottom / (int)factor) * coefficient);
    }
}
