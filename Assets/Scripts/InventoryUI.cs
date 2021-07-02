using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    private bool openInv;
    public Transform inventoryWindow;
    public Transform mainWindow;
    private Stats playerStats;
    public GameObject slot;
    public GameObject CategoryIcon;
    public RecipeCategory[] categories;
    public GameObject RecipeButton;
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
            if (i >= playerStats.inventory.Length)
            {
                return;
            }
            Image icon = inventoryWindow.GetChild(i).Find("Icon")?.gameObject.GetComponent<Image>();
            if (icon != null)
            {
                icon.sprite = (playerStats.inventory[i]?.item ?? null) != null ? playerStats.inventory[i].item.sprite : null;
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
        foreach (var cat in categories)
        {
            foreach (var craft in cat.Recipes)
            {
                if (craft.isAvailable(playerStats.inventory))
                {
                    Transform available = transform.Find("MainPanel/Craft/Available");
                    for (int i = 0; i < available.childCount; i++)
                    {
                        if(available.GetChild(i).GetComponent<RecipeUI>().recipe == craft)
                        {
                            goto IgnoreCreate;
                        }
                    }
                    var recipebutton = Instantiate(RecipeButton, transform.Find("MainPanel/Craft/Available"));
                    recipebutton.GetComponent<RecipeUI>().recipe = craft;
                IgnoreCreate:;
                }
            }
        }
    }
    private void FillCategory()
    {
        Transform categoriesTransform = transform.Find("MainPanel/Craft/Categories");
        categories = Resources.LoadAll<RecipeCategory>("Recipes");
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
