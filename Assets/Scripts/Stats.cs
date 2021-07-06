using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    public int Money;
    public GameObject dropableItem;
    public Weapon weapon;
    public Helmet helmet;
    public Chestplate chestplate;
    public Leggings leggings;
    public Item[] inventory;
    private Rigidbody2D rb;
    private Animator animator;
    private InventoryUI invUI;
    private Vector2 initPos;
    public bool AddItem(Item item)
    {
        if(item.item.stack <= 0)
        {
            item.item.stack = 1;
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].item != null && inventory[i].item == item.item)
            {
                if (inventory[i].quantity + item.quantity <= item.item.stack)
                {
                    inventory[i].quantity += item.quantity;
                    if (InventoryUI.openInv)
                    {
                        InventoryUI.currentPage = 0;
                        invUI.CheckRecipes();
                    }
                    return true;
                }
                else if (item.item.stack - item.quantity > 0)
                {
                    item.quantity -= item.item.stack - inventory[i].quantity;
                    inventory[i].quantity = item.item.stack;

                }
            }

        }
        do
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].item == null || inventory[i].item == null)
                {
                    if (item.quantity <= item.item.stack)
                    {
                        inventory[i] = new Item(item.quantity, item.item);
                        if (InventoryUI.openInv)
                        {
                            invUI.CheckRecipes();
                        }
                        return true;
                    }
                    else
                    {
                        inventory[i] = new Item(item.item.stack, item.item);
                        item.quantity -= item.item.stack;
                    }

                }
            }
        } while (item.quantity > item.item.stack);
        return false;
    }

    public void DropItem(int i, int quantity)
    {
        if (inventory[i].item != null)
        {
            Item temp = inventory[i];
            if (inventory[i].quantity <= quantity)
            {
                inventory[i].quantity = 0;
                inventory[i].item = null;
            }
            else
            {
                inventory[i].quantity -= quantity;
            }
            var drop = Instantiate(dropableItem, transform.position, Quaternion.identity);
            drop.GetComponent<PickableItem>().item = temp;
        }
    }
    public void DropItem(Item item)
    {
        if (item.item != null)
        {
            var drop = Instantiate(dropableItem, transform.position, Quaternion.identity);
            drop.GetComponent<PickableItem>().item = item;
        }
    }
    private void Awake()
    {
        initPos = gameObject.transform.position;
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
        int helmet = this.helmet?.defense ?? 1;
        int chestplate = this.chestplate?.defense ?? 1;
        int leggings = this.leggings?.defense ?? 1;
        int total = helmet * chestplate * leggings;
        damage /= total;
        damage = damage == 0 ? 1 : damage;
        health -= damage;
        if (animator != null)
        {
            animator.SetTrigger("Damage");
        }
        Vector2 push = -((Vector2)owner.transform.position - rb.position).normalized;
        rb.velocity = push != Vector2.zero ? push * power : new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1)) * power;
        if (health <= 0)
        {
            if (CompareTag("Enemy"))
            {
                PlayerLevel.GainXP(Random.Range(30, 60));
                BaseEnemyAI enemAI = GetComponent<BaseEnemyAI>();
                if (enemAI != null && enemAI.PerkStealingGuaranteed)
                {
                    GameObject.Find("Player").GetComponent<PlayerPerks>().perk = enemAI.perk;
                }
            }
            if (inventory.Length > 0)
            {
                for (int i = 0; i < inventory.Length; i++)
                {
                    DropItem(i, inventory[i].quantity);
                }
            }
            if (!CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            else
            {
                owner.GetComponent<Stats>().StartCoroutine(owner.GetComponent<Stats>().Respawn(gameObject));
                gameObject.SetActive(false);
            }
        }
    }
    public IEnumerator Respawn(GameObject player)
    {
        yield return new WaitForSeconds(3);
        GameObject canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
        canvas.SetActive(true);
        player.transform.position = player.GetComponent<Stats>().initPos;
        player.GetComponent<Stats>().health = player.GetComponent<Stats>().MaxHealth;
        player.SetActive(true);
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
