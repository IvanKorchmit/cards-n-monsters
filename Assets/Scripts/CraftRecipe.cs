using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipes/Recipe")]
public class CraftRecipe : ScriptableObject
{
    public Item[] Ingridients;
    public int RequiredLevel;
    public Item result;
    public bool isAvailable(Item[] inventory)
    {
        int valid = 0;
        int index = 0;
        Item currentIngridient = Ingridients[index];
        int quantity = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            if(valid == Ingridients.Length)
            {
                break;
            }
            if(inventory[i].item != null && inventory[i].item == currentIngridient.item)
            {
                quantity += inventory[i].quantity;
            }
            else
            {
                continue;
            }
            if(quantity >= currentIngridient.quantity)
            {
                index++;
                valid++;
                quantity = 0;
                if (valid == Ingridients.Length)
                {
                    break;
                }
                currentIngridient = Ingridients[index];
                i = 0;
                continue;
            }
        }
        return valid == Ingridients.Length;
    }
    public void Consume(Item[] inventory)
    {
        Item ingridient = Ingridients[0];
        int index = 0;
        int remains = ingridient.quantity;
        for (int i = inventory.Length - 1; i >= 0; i--)
        {
            if (inventory[i].quantity <= 0)
            {
                inventory[i].item = null;
            }
            if (remains <= 0)
            {
                i = inventory.Length - 1;
                index++;
                if(index > Ingridients.Length - 1)
                {
                    return;
                }
                ingridient = Ingridients[index];
            }
            if(inventory[i].item != null && inventory[i].item == ingridient.item)
            {
                if(inventory[i].quantity > ingridient.quantity)
                {
                    remains = 0;
                    inventory[i].quantity -= ingridient.quantity;
                    continue;
                }
                else if (inventory[i].quantity > remains)
                {
                    inventory[i].quantity -= remains;
                    remains = 0;
                    continue;
                }
                else
                {
                    remains -= inventory[i].quantity;
                    inventory[i].quantity = 0;
                }
            }
        }
    }
}
