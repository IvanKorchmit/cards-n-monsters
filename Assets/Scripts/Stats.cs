using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    public float MaxHealth => maxHealth;
    public float Health => health;
    private void Start()
    {
        maxHealth = health;
    }
    public void Damage(float damage)
    {
        health -= damage;
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
