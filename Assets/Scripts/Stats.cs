using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    private Animator animator;
    public float MaxHealth => maxHealth;
    public float Health => health;
    private void Start()
    {
        maxHealth = health;
        animator = GetComponent<Animator>();
    }
    public void Damage(float damage)
    {
        health -= damage;
        if (animator != null)
        {
            animator.SetTrigger("Damage");
        }
        if(health <= 0)
        {
            if (CompareTag("Enemy"))
            {
                GameObject.Find("Player").GetComponent<PlayerPerks>().perk = GetComponent<BaseEnemyAI>().perk;
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
    void Damage(float damage);
}
