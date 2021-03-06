using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class BaseItem : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite sprite;
    public int cost;
    public int id;
    public int stack;
    public virtual void Use(GameObject owner, ref bool consumed)
    {
        Debug.Log("used item");
    }
    public override string ToString()
    {
        return $"{name} {id}";
    }
}
public class Weapon : BaseItem
{
    public int damage;
}
public class Armor : BaseItem
{
    public int defense;
}

public static class Settings
{
    public static float musicVolume = 1.0f;
    public static float SFX = 1.0f;
    public static bool isAzerty;

}
