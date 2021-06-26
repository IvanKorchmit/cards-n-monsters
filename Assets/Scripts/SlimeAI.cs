using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    public PerkClass perk;
    private GameObject player;
    public bool IsDashing;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("UsePerk", 0, 1);
    }
    private void UsePerk()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= 10)
        {
            perk.Use(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetComponent<Rigidbody2D>().velocity.magnitude > 2)
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(5);
        }
    }
}
