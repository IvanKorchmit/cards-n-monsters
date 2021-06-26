﻿using UnityEngine;
[CreateAssetMenu(fileName ="New Dash",menuName ="Perks/Dash")]
public class Dash : PerkClass
{
    public override void Use(GameObject owner)
    {
        if(owner.CompareTag("Enemy"))
        {
            Vector2 player = GameObject.FindGameObjectWithTag("Player").transform.position;
            Debug.Log("Trying to jump to player");
            Rigidbody2D rb = owner.GetComponent<Rigidbody2D>();
            rb.velocity = (player - rb.position).normalized * 20;
        }
    }
}