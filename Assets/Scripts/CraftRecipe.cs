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
            if(inventory[i] != null && inventory[i].item == currentIngridient.item)
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
}
