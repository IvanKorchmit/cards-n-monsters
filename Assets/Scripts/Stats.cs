using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    private Rigidbody2D rb;
    private Animator animator;
    public int MaxHealth => maxHealth;
    public int Health => health;
    private void Start()
    {
        maxHealth = health;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Damage(int damage, GameObject owner)
    {
        health -= damage;
        if (animator != null)
        {
            animator.SetTrigger("Damage");
        }
        rb.velocity = -((Vector2)owner.transform.position - rb.position).normalized * 8f;
        if(health <= 0)
        {
            if (CompareTag("Enemy"))
            {
                BaseEnemyAI enemAI = GetComponent<BaseEnemyAI>();
                if (enemAI.PerkStealingGuaranteed)
                {
                    GameObject.Find("Player").GetComponent<PlayerPerks>().perk = enemAI.perk;
                }   
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        
    }
}


interface IDamagable
{
    void Damage(int damage, GameObject owner);
}
