using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ReassignID : EditorWindow
{
    [MenuItem("Window/Reassign ID")]
    static void Init()
    {
        ReassignID window = (ReassignID)GetWindow(typeof(ReassignID));
        window.Show();
    }
    private void OnGUI()
    {
        GUILayout.Label("ID Reassigner", EditorStyles.boldLabel);
        if(GUILayout.Button("Reassign Item IDs"))
        {
            Reassign();
        }
    }
    private void Reassign()
    {
        BaseItem[] items = Resources.LoadAll<BaseItem>("Items");
        Debug.Log("Reassigning items");
        for (int i = 0; i < items.Length; i++)
        {
            items[i].id = i;
            Debug.Log(items[i].name + " " + items[i].id);
        }
    }
}