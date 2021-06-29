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
    private void Start()
    {
        CheckInventory();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        FillInventory();
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
    }
    private void FillInventory()
    {
        if (playerStats.inventory != null)
        {
            for (int i = 0; i < playerStats.inventory.Length; i++)
            {
                slot = Instantiate(slot, inventoryWindow);
            }
        }
    }
    private void UpdateInventory()
    {
        for (int i = 0; i < inventoryWindow.childCount; i++)
        {
            if(i >= playerStats.inventory.Length)
            {
                return;
            }
            Image icon = inventoryWindow.GetChild(i).Find("Icon").gameObject.GetComponent<Image>();
            icon.sprite = playerStats.inventory[i].item.sprite;
        }
    }
}
