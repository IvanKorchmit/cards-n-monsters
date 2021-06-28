using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int cost;
    public int id;
    public virtual void Use(GameObject owner, ref bool consumed)
    {
        Debug.Log("used item");
    }
    
}
public class Potion : BaseItem
{
    public override void Use(GameObject owner, ref bool consumed)
    {
        consumed = true;
    }
}
[System.Serializable]
public class Item
{
    public BaseItem item;
    public int quantity;
}