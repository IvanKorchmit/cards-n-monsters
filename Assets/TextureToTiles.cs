using UnityEngine;
using UnityEngine.Tilemaps;
public class TextureToTiles : MonoBehaviour
{
    public Texture2D texture;

    public Tilemap tilemap;
    public Tilemap tallGrass;
    public ColorToTile[] colorToTiles;
    public void Clear(Tilemap tm)
    {
        for (int x = 0; x < tm.size.x; x++)
        {
            for (int y = 0; y < tm.size.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0) + tm.origin;
                tm.SetTile(pos, null);
            }
        }
    }
    public void Draw(Tilemap tm)
    {
        Texture2D t = texture;
        for (int x = 0; x < t.width; x++)
        {
            for (int y = 0; y < t.height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0) + tm.origin;
                Color color = t.GetPixel(x, y);
                foreach (var c in colorToTiles)
                {
                    if (c.color == color)
                    {
                        tm.SetTile(pos, c.tiles[Random.Range(0, c.tiles.Length - 1)]);
                        if ((c.tallGrass?.Length ?? 0) > 0)
                        {
                            tallGrass.SetTile(pos, c.tallGrass[Random.Range(0, c.tallGrass.Length - 1)]);
                        }
                        break;
                    }
                }
            }
        }
    }
}
[System.Serializable]
public class ColorToTile
{
    public Color color;

    public Tile[] tiles;

    public Tile[] tallGrass;

}
