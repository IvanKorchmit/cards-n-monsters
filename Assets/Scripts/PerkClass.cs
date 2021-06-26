using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkClass : ScriptableObject
{
    public virtual void Use(GameObject owner)
    {
        Debug.Log("Used perk");
    }
}
