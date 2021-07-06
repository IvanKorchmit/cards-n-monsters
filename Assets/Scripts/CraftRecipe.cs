using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/Recipe")]
public class CraftRecipe : ScriptableObject
{
    private const float RADIUS = 2f;
    public enum Workshop
    {
        none,anvil,furnace,boiler
    }
    public Workshop workshop = Workshop.none;
    public Item[] Ingridients;
    public int RequiredLevel;
    public Item result;
    public bool isAvailable(Item[] inventory)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int index = 0;
        Item ing = Ingridients[index];
        int q = 0;
        int valid = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            if(ing.item == inventory[i].item)
            {
                for (int x = 0; x < inventory[i].quantity; x++)
                {
                    if(q >= ing.quantity)
                    {
                        break;
                    }   
                    q++;
                }
            }
            if (q >= ing.quantity)
            {
                i = -1;
                q = 0;
                index++;
                valid++;
                if (index >= Ingridients.Length)
                {
                    break;
                }
                ing = Ingridients[index];
            }
        }
        if (workshop == Workshop.none)
        {
            return valid == Ingridients.Length;
        }
        else if (workshop == Workshop.furnace)
        {
            bool atFurnace = false;
            RaycastHit2D[] ray = Physics2D.CircleCastAll(player.transform.position, RADIUS, Vector2.zero, 0);
            foreach (var r in ray)
            {
                if(r.collider != null && r.collider.CompareTag("Furnace"))
                {
                    atFurnace = true;
                    break;
                }
            }
            return valid == Ingridients.Length && atFurnace;
        }
        else if (workshop == Workshop.anvil)
        {
            bool atAnvil = false;
            RaycastHit2D[] ray = Physics2D.CircleCastAll(player.transform.position, RADIUS, Vector2.zero, 0);
            foreach (var r in ray)
            {
                if (r.collider != null && r.collider.CompareTag("Anvil"))
                {
                    atAnvil = true;
                    break;
                }
            }
            return valid == Ingridients.Length && atAnvil;
        }
        else if (workshop == Workshop.boiler)
        {
            bool atBoiler = false;
            RaycastHit2D[] ray = Physics2D.CircleCastAll(player.transform.position, RADIUS, Vector2.zero, 0);
            foreach (var r in ray)
            {
                if (r.collider != null && r.collider.CompareTag("Boiler"))
                {
                    atBoiler = true;
                    break;
                }
            }
            return valid == Ingridients.Length && atBoiler;
        }
        return false;
    }
    public void Consume(Item[] inventory)
    {
        for (int i = 0; i < Ingridients.Length; i++)
        {
            int quantity = Ingridients[i].quantity;
            for (int x = 0; x < inventory.Length; x++)
            {
                if(Ingridients[i].item == inventory[x].item)
                {
                    for (int y = 0; y <= inventory[x].quantity; y++)
                    {
                        if(quantity <= 0)
                        {
                            break;
                        }
                        quantity--;
                        inventory[x].quantity--;
                    }
                    if(quantity <= 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}
