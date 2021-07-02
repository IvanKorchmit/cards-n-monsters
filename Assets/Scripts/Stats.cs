using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public GameObject dropableItem;
    public Weapon weapon;
    public Item[] inventory;
    private Rigidbody2D rb;
    private Animator animator;
    private InventoryUI invUI;
    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] != null && inventory[i].item == item.item)
            {
                inventory[i].quantity += item.quantity;
                if (InventoryUI.openInv)
                {
                    InventoryUI.currentPage = 0;
                    invUI.CheckRecipes();
                }
                return true;
            }
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null || inventory[i].item == null)
            {
                inventory[i] = new Item(item.quantity, item.item);
                if (InventoryUI.openInv)
                {
                    invUI.CheckRecipes();
                }
                return true;
            }
        }
        return false;
    }



    public int MaxHealth => maxHealth;
    public int Health => health;
    private void Start()
    {
        invUI = GameObject.Find("Canvas").GetComponent<InventoryUI>();
        maxHealth = health;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Damage(int damage, GameObject owner, float power)
    {
        health -= damage;
        if (animator != null)
        {
            animator.SetTrigger("Damage");
        }
        Vector2 push = -((Vector2)owner.transform.position - rb.position).normalized;
        rb.velocity =  push != Vector2.zero ? push * power: new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1)) * power;
        if(health <= 0)
        {
            if (CompareTag("Enemy"))
            {
                PlayerLevel.GainXP(70);
                BaseEnemyAI enemAI = GetComponent<BaseEnemyAI>();
                if (enemAI.PerkStealingGuaranteed)
                {
                    GameObject.Find("Player").GetComponent<PlayerPerks>().perk = enemAI.perk;
                }   
            }
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}


interface IDamagable
{
    void Damage(int damage, GameObject owner, float power);
}
