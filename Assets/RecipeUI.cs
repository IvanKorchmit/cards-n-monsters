using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RecipeUI : MonoBehaviour
{
    public CraftRecipe recipe;
    public GameObject ingridientIcon;
    public Stats Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        Init();
    }
    public void Init()
    {
        Transform ingridients = transform.Find("Ingridients");
        transform.Find("Result").GetComponent<Image>().sprite = recipe.result.item.sprite;
        for (int i = 0; i < recipe.Ingridients.Length; i++)
        {
            Image ingridientIcon = Instantiate(this.ingridientIcon, ingridients).GetComponent<Image>();
            ingridientIcon.rectTransform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = recipe.Ingridients[i].quantity.ToString();
            ingridientIcon.sprite = recipe.Ingridients[i].item.sprite;
        }
    }
    public void Click()
    {
        if (recipe.isAvailable(Player.inventory))
        {
            recipe.Consume(Player.inventory);
            if(!Player.AddItem(recipe.result))
            {
                Player.DropItem(recipe.result);
            }
        }
    }
}
