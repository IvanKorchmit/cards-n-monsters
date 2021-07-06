using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RecipeUI : MonoBehaviour
{
    public CraftRecipe recipe;
    public GameObject ingridientIcon;
    private Stats Player;
    public Sprite anvil;
    public Sprite boiler;
    public Sprite furnace;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        Init();
    }
    public void Init()
    {
        Transform ingridients = transform.Find("Ingridients");
        transform.Find("Result").GetComponent<Image>().sprite = recipe.result.item.sprite;
        switch (recipe.workshop)
        {
            case CraftRecipe.Workshop.none:
                Destroy(transform.Find("Workshop").gameObject);
                break;
            case CraftRecipe.Workshop.anvil:
                transform.Find("Workshop").GetComponent<Image>().sprite = anvil;

                break;
            case CraftRecipe.Workshop.furnace:
                transform.Find("Workshop").GetComponent<Image>().sprite = furnace;

                break;
            case CraftRecipe.Workshop.boiler:
                transform.Find("Workshop").GetComponent<Image>().sprite = boiler;

                break;
            default:
                break;
        }
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
            GameObject.Find("Canvas").GetComponent<InventoryUI>().CheckRecipes();
        }
    }
}
