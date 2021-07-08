using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TextureToTiles))]
public class EditorTexToTile : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TextureToTiles tex = (TextureToTiles)target;
        if(GUILayout.Button("Load map"))
        {
            tex.Clear(tex.tilemap);
            tex.Clear(tex.tallGrass);
            tex.Draw(tex.tilemap);
        }
    }
}
