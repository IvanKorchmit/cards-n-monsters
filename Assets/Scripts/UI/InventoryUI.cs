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
    public Transform containerWindow;
    public Transform containerSection;
    public Transform mainWindow;
    public Canvas canvas;
    public CanvasScaler scaler;
    private Stats playerStats;
    public GameObject slot;
    public GameObject CategoryIcon;
    public GameObject RecipeButton;
    public static RecipeCategory currentCategory;
    public static Stats npc;
    public TextMeshProUGUI coinsPlayer;
    public TextMeshProUGUI coinsNPC;

    public RectTransform infoWindow;

    private void Start()
    {
        scaler = GetComponent<CanvasScaler>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        canvas = GetComponent<Canvas>();
        FillInventory();
        FillCategory();
        CheckInventory();
    }
    private void Update()
    {
        if (npc != null)
        {
            if(Vector2.Distance(playerStats.gameObject.transform.position,npc.gameObject.transform.position) > 2f)
            {
                openInv = false;
                CheckInventory();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            openInv = !openInv;
            CheckInventory();
        }
        if(openInv)
        {
            UpdateInventory();
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }
    }
    public void CheckInventory()
    {
        mainWindow.gameObject.SetActive(openInv);
        containerSection.gameObject.SetActive(npc != null);
        if (openInv)
        {
            if(transform.Find("PressE") != null)
            {
                Destroy(transform.Find("PressE").gameObject);
            }
            CheckRecipes();
            if (npc != null)
            {
                FillInventoryContainer();
            }
        }
        else
        {
            ChangeCategory();
            if (npc != null)
            {
                npc.GetComponent<npcAI>().isBusy = false;
                npc = null;
            }
        }
    }
    public void NextPage()
    {
        currentPage = currentPage + 1 < currentCategory.Recipes.Length ? currentPage + 1 : currentCategory.Recipes.Length;
        CheckRecipes();
    }
    public void PreviousPage()
    {
        currentPage = currentPage - 1 > 0 ? currentPage - 1 : 0;
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
                var slot = Instantiate(this.slot, inventoryWindow);
                slot.GetComponent<Image>().SetNativeSize();
                slot.GetComponent<DragDropItem>().slotNumber = i;
            }
        }
    }
    public void FillInventoryContainer()
    {
        for (int i = 0; i < containerWindow.childCount; i++)
        {
            Destroy(containerWindow.GetChild(i).gameObject);
        }
        if (npc.inventory != null)
        {
            for (int i = 0; i < npc.inventory.Length; i++)
            {
                var slot = Instantiate(this.slot, containerWindow);
                slot.GetComponent<Image>().SetNativeSize();
                slot.GetComponent<DragDropItem>().slotNumber = i;
                slot.GetComponent<DragDropItem>().type = DragDropItem.SpecializedSlot.trade;
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
        if (npc != null)
        {
            for (int i = 0; i < containerWindow.childCount; i++)
            {
                if (i > npc.inventory.Length - 1)
                {
                    return;
                }
                if (npc.inventory[i].quantity <= 0)
                {
                    npc.inventory[i].item = null;
                }
                Image icon = containerWindow.GetChild(i).Find("Icon")?.gameObject.GetComponent<Image>();
                if (icon != null)
                {
                    TextMeshProUGUI quantity = containerWindow.GetChild(i).Find("Icon/Quantity").GetComponent<TextMeshProUGUI>();
                    Item it = npc.inventory[i];
                    quantity.text = it.quantity > 1 ? $"{it.quantity}" : "";



                    icon.sprite = it.item != null ? it.item.sprite : null;
                    icon.color = icon.sprite != null ? Color.white : Color.clear;
                    icon.SetNativeSize();
                }
            }
        }
        coinsPlayer.text = playerStats.Money.ToString();
        if (npc != null)
        {
            coinsNPC.text = npc.Money.ToString();
        }
    }
    public void ChangeCategory()
    {
        Transform available = transform.Find("MainPanel/Craft/Available");
        int childCount = available.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = available.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
    public void CheckRecipes()
    {
        ChangeCategory();
        Transform available = transform.Find("MainPanel/Craft/Available");
        int unavailable = 0;
        for (int x = currentPage; available.childCount < 2; x++)
        {
            if (x >= currentCategory.Recipes.Length)
            {
                currentPage = 0;
                x = currentPage;
                if(unavailable >= currentCategory.Recipes.Length)
                {
                    break;
                }
            }
            CraftRecipe craft = currentCategory.Recipes[x];
            if (craft.isAvailable(playerStats.inventory))
            {
                GameObject recipebutton = Instantiate(RecipeButton, available);
                recipebutton.GetComponent<RecipeUI>().recipe = craft;
            }
            else
            {
                unavailable++;
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
    public void DisplayInfo(Item item)
    {
        if (item.item == null)
        {
            infoWindow.gameObject.SetActive(false);
            return;
        }
        else
        {
            infoWindow.gameObject.SetActive(true);
        }
        Vector2 res;    
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out res);
        infoWindow.anchoredPosition = res;
        Debug.Log(Screen.width);
        infoWindow.anchoredPosition = new Vector2(infoWindow.anchoredPosition.x, Mathf.Clamp(infoWindow.anchoredPosition.y, 0, Screen.height) + 32);
        infoWindow.Find("Name/Text").GetComponent<TextMeshProUGUI>().text = item.item.name;
        infoWindow.Find("Cost/Text").GetComponent<TextMeshProUGUI>().text = $"{item.quantity}x{item.item.cost}$ {item.item.cost*item.quantity}$";
        infoWindow.Find("Description/Text").GetComponent<TextMeshProUGUI>().text = item.item.description;
    }
}
