using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    public static int currentPage;
    public static bool openInv;
    public Transform inventoryWindow;
    public Transform mainWindow;
    private Stats playerStats;
    public GameObject slot;
    public GameObject CategoryIcon;
    public GameObject RecipeButton;
    public static RecipeCategory currentCategory;
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        FillInventory();
        FillCategory();
        CheckInventory();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            openInv = !openInv;
            CheckInventory();
        }
        if(openInv)
        {
            UpdateInventory();
        }
    }
    private void CheckInventory()
    {
        mainWindow.gameObject.SetActive(openInv);
        if (openInv)
        {
            CheckRecipes();
        }
        else
        {
            ChangeCategory();
        }
    }
    public void NextPage()
    {
        currentPage = currentPage + 2 < currentCategory.Recipes.Length - 1 ? currentPage + 2 : currentCategory.Recipes.Length - 1;
        CheckRecipes();
    }
    public void PreviousPage()
    {
        currentPage = currentPage - 2 > 1 ? currentPage - 2 : 0;
        CheckRecipes();

    }
    private void FillInventory()
    {
        for (int i = 0; i < inventoryWindow.childCount; i++)
        {
            Destroy(inventoryWindow.GetChild(i).gameObject);
        }
        if (playerStats.inventory != null)
        {
            for (int i = 0; i < playerStats.inventory.Length; i++)
            {
                slot = Instantiate(this.slot, inventoryWindow);
                slot.GetComponent<Image>().SetNativeSize();
                slot.GetComponent<DragDropItem>().slotNumber = i;
            }
        }
    }
    private void UpdateInventory()
    {
        for (int i = 0; i < inventoryWindow.childCount; i++)
        {
            if(playerStats.inventory[i].quantity <= 0)
            {
                playerStats.inventory[i].item = null;
            }
            if (i >= playerStats.inventory.Length)
            {
                return;
            }
            Image icon = inventoryWindow.GetChild(i).Find("Icon")?.gameObject.GetComponent<Image>();
            TextMeshProUGUI quantity = inventoryWindow.GetChild(i).Find("Icon/Quantity").GetComponent<TextMeshProUGUI>();
            if (icon != null)
            {
                Item it = playerStats.inventory[i];
                quantity.text = it.quantity > 1 ? $"{it.quantity}" : "";



                icon.sprite = it.item != null ? it.item.sprite : null;
                icon.color = icon.sprite != null ? Color.white : Color.clear;
                icon.SetNativeSize();
            }
        }
    }
    public void ChangeCategory()
    {
        Transform available = transform.Find("MainPanel/Craft/Available");
        for (int i = 0; i < available.childCount; i++)
        {
            Destroy(available.GetChild(i).gameObject);
        }
    }
    public void CheckRecipes()
    {
        ChangeCategory();
        Transform available = transform.Find("MainPanel/Craft/Available");
        for (int x = currentPage; x < currentPage + 2; x++)
        {
            if (x > currentCategory.Recipes.Length - 1)
            {
                break;
            }   
            CraftRecipe craft = currentCategory.Recipes[x];
            if (craft.isAvailable(playerStats.inventory))
            {
                GameObject recipebutton = Instantiate(RecipeButton, available);
                recipebutton.GetComponent<RecipeUI>().recipe = craft;
            }
            else
            {
                currentPage++;
            }
        }
    }
    private void FillCategory()
    {
        Transform categoriesTransform = transform.Find("MainPanel/Craft/Categories");
        RecipeCategory[] categories = Resources.LoadAll<RecipeCategory>("Recipes");
        currentCategory = categories[0];
        for (int i = 0; i < categoriesTransform.childCount; i++)
        {
            Destroy(categoriesTransform.GetChild(i).gameObject);
        }
        foreach (var cat in categories)
        {
            GameObject category = Instantiate(CategoryIcon, categoriesTransform);
            category.transform.Find("Icon").GetComponent<CategoryUI>().cat = cat;
        }
    }

}
