using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CategoryUI : MonoBehaviour
{
    public RecipeCategory cat;

    private void Start()
    {
        GetComponent<Image>().sprite = cat.icon;
    }

    public void OnClick()
    {
        InventoryUI.currentCategory = cat;
        GameObject.Find("Canvas").GetComponent<InventoryUI>().CheckRecipes();
    }

}
