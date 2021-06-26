using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    public PerkClass perk;
    private void Start()
    {
        InvokeRepeating("UsePerk", 0, 1);
    }
    private void UsePerk()
    {
        perk.Use(gameObject);
    }
}
