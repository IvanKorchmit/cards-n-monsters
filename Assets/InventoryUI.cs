using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool openInv;
    private Transform inventoryWindow;
    private void Start()
    {
        inventoryWindow = transform.Find("Inventory Panel");
        CheckInventory();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            openInv = !openInv;
            CheckInventory();
        }
    }
    private void CheckInventory()
    {
        inventoryWindow.gameObject.SetActive(openInv);
    }
}
