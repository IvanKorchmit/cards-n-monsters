using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;
using UnityEngine.UI;
public class DragDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public enum SpecializedSlot
    {
        inventory, weapon, helmet, chest, leggings, trade

    }
    public SpecializedSlot type;
    public static SpecializedSlot comingFrom;
    public static Item dragItem;
    public static Stats Player;
    public static Canvas canvas;
    private static RectTransform item;
    private static CanvasGroup originSlot;
    private static int origin;
    public int slotNumber;
    private Image icon;
    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        icon = transform.Find("Icon").GetComponent<Image>();
    }
    private void OnGUI()
    {
        switch (type)
        {
            case SpecializedSlot.weapon:
                icon.sprite = Player.weapon?.sprite ?? null;
                icon.color = Player.weapon?.sprite ?? null != null ? Color.white : Color.clear;
                icon.SetNativeSize();
                break;
            case SpecializedSlot.helmet:
                break;
            case SpecializedSlot.chest:
                break;
            case SpecializedSlot.leggings:
                break;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (type == SpecializedSlot.inventory)
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                dragItem = Player.inventory[slotNumber];
                if (dragItem.item == null) return;
                origin = slotNumber;
            }
            else
            {
                dragItem = new Item(1, Player.inventory[slotNumber].item);
                if (dragItem.item == null) return;
                origin = slotNumber;
            }
        }
        else if (type == SpecializedSlot.weapon)
        {
            dragItem = new Item(1, Player.weapon);
        }
        else if (type == SpecializedSlot.trade)
        {
            dragItem = InventoryUI.npc.inventory[slotNumber];
            if (dragItem.item == null) return;
            origin = slotNumber;
        }

        comingFrom = type;

        originSlot = transform.Find("Icon").GetComponent<CanvasGroup>();
        originSlot.alpha = 0.5f;
        item = Instantiate(transform.Find("Icon").gameObject, canvas.transform).GetComponent<RectTransform>();
        item.SetParent(canvas.transform);
        item.GetComponent<AspectRatioFitter>().enabled = false;


        item.anchorMin = new Vector2(0, 0);
        item.anchorMax = new Vector2(0, 0);


        item.GetComponent<Image>().SetNativeSize();
        item.anchoredPosition = Input.mousePosition / canvas.scaleFactor;
        item.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            item.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                switch (comingFrom)
                {
                    case SpecializedSlot.weapon:
                    case SpecializedSlot.inventory:
                        if (Player.inventory[origin] == dragItem)
                        {
                            Player.DropItem(origin, dragItem.quantity);
                        }
                        else
                        {
                            Player.inventory[origin].quantity -= dragItem.quantity;
                            Player.DropItem(dragItem);
                        }
                        break;
                }
            }
            Destroy(item.gameObject);
        }
        originSlot.alpha = 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (dragItem.item == null || dragItem.item == null)
        {
            return;
        }
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Slot"))
        {
            Item temp = Player.inventory[slotNumber];
            switch (type)
            {
                case SpecializedSlot.inventory:
                    if (comingFrom == SpecializedSlot.inventory)
                    {
                        if (Player.inventory[slotNumber].item != null && Player.inventory[slotNumber].item != dragItem.item)
                        {
                            Player.inventory[slotNumber] = dragItem;
                            Player.inventory[origin] = temp;
                        }
                        else if (Player.inventory[slotNumber].item == dragItem.item)
                        {
                            if (Player.inventory[slotNumber].quantity + dragItem.quantity < dragItem.item.stack)
                            {
                                Player.inventory[origin].quantity -= dragItem.quantity;
                                Player.inventory[slotNumber].quantity += dragItem.quantity;
                            }
                            else
                            {
                                Player.inventory[origin].quantity -= dragItem.quantity;
                                dragItem.quantity -= dragItem.item.stack;
                                Player.inventory[slotNumber].quantity = dragItem.item.stack;
                                if (!Player.AddItem(dragItem))
                                {
                                    Player.DropItem(dragItem);
                                }
                            }
                        }
                        else if (Player.inventory[slotNumber].item == null)
                        {
                            Player.inventory[origin].quantity -= dragItem.quantity;
                            Player.inventory[slotNumber] = dragItem;
                        }
                    }
                    else if (comingFrom == SpecializedSlot.trade)
                    {
                        int cost = 0;
                        if (Trade.canAfford(dragItem, Player.Money, ref cost))
                        {
                            InventoryUI.npc.inventory[origin] = temp;
                            Player.inventory[slotNumber] = dragItem;
                            Player.Money -= cost;
                            InventoryUI.npc.Money += cost;
                        }
                    }
                    else if (comingFrom == SpecializedSlot.weapon && temp.item is Weapon || temp.item == null)
                    {
                        Player.inventory[slotNumber] = dragItem;
                        Weapon weap = temp.item as Weapon;
                        Player.weapon = weap;
                    }
                    else if (!(temp.item is Weapon))
                    {
                        Debug.Log("Isn't");
                        break;
                    }
                    break;
                case SpecializedSlot.weapon:
                    if (dragItem.item is Weapon w)
                    {
                        if (comingFrom == SpecializedSlot.inventory)
                        {
                            Player.inventory[origin] = new Item(1, Player.weapon);
                            Player.weapon = w;
                        }
                        else if (comingFrom == SpecializedSlot.trade)
                        {

                            break;
                        }
                    }
                    break;
                case SpecializedSlot.helmet:
                    break;
                case SpecializedSlot.chest:
                    break;
                case SpecializedSlot.leggings:
                    break;
                case SpecializedSlot.trade:
                    if (comingFrom == SpecializedSlot.inventory)
                    {
                        int cost = 0;
                        if (Trade.canAfford(dragItem, InventoryUI.npc.Money, ref cost))
                        {
                            if (InventoryUI.npc.AddItem(dragItem))
                            {
                                Player.inventory[origin].item = null;
                                Player.inventory[origin].quantity = 0;
                                Player.Money += cost;
                                InventoryUI.npc.Money -= cost;
                            }
                        }
                    }
                    break;
            }

        }

    }
}
