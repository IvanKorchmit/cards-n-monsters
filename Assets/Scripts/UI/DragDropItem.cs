using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;
using UnityEngine.UI;
public class DragDropItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerClickHandler
{
    InventoryUI iUI;
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
        iUI = canvas.GetComponent<InventoryUI>();
    }
    private void OnGUI()
    {
        switch (type)
        {
            case SpecializedSlot.weapon:
                icon.sprite = Player.weapon?.sprite ?? null;
                icon.color = Player.weapon != null ? Color.white : Color.clear;
                icon.SetNativeSize();
                break;
            case SpecializedSlot.helmet:
                icon.sprite = Player.helmet?.sprite ?? null;
                icon.color = Player.helmet != null ? Color.white : Color.clear;
                icon.SetNativeSize();
                break;
            case SpecializedSlot.chest:
                icon.sprite = Player.chestplate?.sprite ?? null;
                icon.color = Player.chestplate != null ? Color.white : Color.clear;
                break;
            case SpecializedSlot.leggings:
                icon.sprite = Player.leggings?.sprite ?? null;
                icon.color = Player.leggings != null ? Color.white : Color.clear;
                break;
        }
        PointerEventData p = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };
        p.position = Input.mousePosition;

        List<RaycastResult> res = new List<RaycastResult>();
        EventSystem.current.RaycastAll(p, res);
        if (res.Count > 0)
        {
            foreach (var gui in res)
            {
                if (gui.gameObject.TryGetComponent(out DragDropItem drag))
                {

                    if (drag.type == SpecializedSlot.inventory)
                    {
                        iUI.DisplayInfo(Player.inventory[drag.slotNumber]);
                        goto IgnoreDeactivating;
                    }
                    else if (drag.type == SpecializedSlot.trade)
                    {
                        iUI.DisplayInfo(InventoryUI.npc.inventory[drag.slotNumber]);
                        goto IgnoreDeactivating;
                    }
                }
            }
        }
        iUI.infoWindow.gameObject.SetActive(false);
    IgnoreDeactivating:;
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
        else if (type == SpecializedSlot.helmet)
        {
            dragItem = new Item(1, Player.helmet);
        }
        else if (type == SpecializedSlot.chest)
        {
            dragItem = new Item(1, Player.chestplate);
        }
        else if (type == SpecializedSlot.leggings)
        {
            dragItem = new Item(1, Player.leggings);
        }
        else if (type == SpecializedSlot.trade)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                dragItem = new Item(1, InventoryUI.npc.inventory[slotNumber].item);
                if (dragItem.item == null) return;
                origin = slotNumber;
            }
            else
            {
                dragItem = InventoryUI.npc.inventory[slotNumber];
                if (dragItem.item == null) return;
                origin = slotNumber;
            }
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
                            if (TryGetComponent(out SoundEvents sound))
                            {
                                sound.PlayPitched(SoundsStatic.PickUp);
                            }
                            else
                            {
                                gameObject.AddComponent<SoundEvents>().PlayPitched(SoundsStatic.PickUp);
                            }
                        }
                        else
                        {
                            Player.inventory[origin].quantity -= dragItem.quantity;
                            Player.DropItem(dragItem);
                            if (TryGetComponent(out SoundEvents sound))
                            {
                                sound.PlayPitched(SoundsStatic.PickUp);
                            }
                            else
                            {
                                gameObject.AddComponent<SoundEvents>().PlayPitched(SoundsStatic.PickUp);
                            }
                        }
                        break;
                }
            }
            Destroy(item.gameObject);
        }
        originSlot.alpha = 1;
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
                    {
                        if (comingFrom == SpecializedSlot.inventory)
                        {
                            if (Player.inventory[slotNumber].item != null && Player.inventory[slotNumber].item != dragItem.item)
                            {
                                Player.inventory[slotNumber] = dragItem;
                                Player.inventory[origin] = temp;
                            }
                            else if (Player.inventory[slotNumber].item == dragItem.item && Player.inventory[slotNumber].quantity < dragItem.item.stack)
                            {
                                if (Player.inventory[slotNumber].quantity + dragItem.quantity <= dragItem.item.stack)
                                {
                                    Player.inventory[origin].quantity -= dragItem.quantity;
                                    Player.inventory[slotNumber].quantity += dragItem.quantity;
                                }
                                else
                                {
                                    dragItem.quantity = (Player.inventory[slotNumber].quantity + dragItem.quantity) - dragItem.item.stack;
                                    Player.inventory[origin].quantity = dragItem.quantity;
                                    Player.inventory[slotNumber].quantity = dragItem.item.stack;
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
                                if (InventoryUI.npc.inventory[origin].item == temp.item || temp.item == null)
                                {
                                    Player.Money -= cost;
                                    InventoryUI.npc.Money += cost;
                                    if (Player.inventory[slotNumber].item != null && Player.inventory[slotNumber].item != dragItem.item)
                                    {
                                        Player.inventory[slotNumber] = dragItem;
                                        InventoryUI.npc.inventory[origin] = temp;
                                    }
                                    else if (Player.inventory[slotNumber].item == dragItem.item && Player.inventory[slotNumber].quantity < dragItem.item.stack)
                                    {
                                        if (Player.inventory[slotNumber].quantity + dragItem.quantity <= dragItem.item.stack)
                                        {
                                            InventoryUI.npc.inventory[origin].quantity -= dragItem.quantity;
                                            Player.inventory[slotNumber].quantity += dragItem.quantity;
                                        }
                                        else
                                        {
                                            dragItem.quantity = (Player.inventory[slotNumber].quantity + dragItem.quantity) - dragItem.item.stack;
                                            InventoryUI.npc.inventory[origin].quantity = dragItem.quantity;
                                            Player.inventory[slotNumber].quantity = dragItem.item.stack;
                                        }
                                    }
                                    else if (Player.inventory[slotNumber].item == null)
                                    {
                                        InventoryUI.npc.inventory[origin].quantity -= dragItem.quantity;
                                        Player.inventory[slotNumber] = dragItem;
                                    }
                                }
                            }
                        }
                        else if (comingFrom == SpecializedSlot.weapon && temp.item is Weapon || temp.item == null)
                        {
                            Player.inventory[slotNumber] = dragItem;
                            Weapon weap = temp.item as Weapon;
                            Player.weapon = weap;
                        }
                        else if (comingFrom == SpecializedSlot.helmet && temp.item is Helmet || temp.item == null)
                        {
                            Player.inventory[slotNumber] = dragItem;
                            Helmet hel = temp.item as Helmet;
                            Player.helmet = hel;
                        }
                        else if (comingFrom == SpecializedSlot.chest && temp.item is Chestplate || temp.item == null)
                        {
                            Player.inventory[slotNumber] = dragItem;
                            Chestplate ch = temp.item as Chestplate;
                            Player.chestplate = ch;
                        }
                        else if (comingFrom == SpecializedSlot.leggings && temp.item is Leggings || temp.item == null)
                        {
                            Player.inventory[slotNumber] = dragItem;
                            Leggings lg = temp.item as Leggings;
                            Player.leggings = lg;
                        }
                        else if (!(temp.item is Weapon) || !(temp.item is Helmet) || !(temp.item is Chestplate) || !(temp.item is Leggings))
                        {
                            Debug.Log("Isn't");
                            break;
                        }
                        break;
                    }
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
                    if (dragItem.item is Helmet h)
                    {
                        if (comingFrom == SpecializedSlot.inventory)
                        {
                            Player.inventory[origin] = new Item(1, Player.helmet);
                            Player.helmet = h;
                        }
                        else if (comingFrom == SpecializedSlot.trade)
                        {

                            break;
                        }
                    }
                    break;
                case SpecializedSlot.chest:
                    if (dragItem.item is Chestplate c)
                    {
                        if (comingFrom == SpecializedSlot.inventory)
                        {
                            Player.inventory[origin] = new Item(1, Player.chestplate);
                            Player.chestplate = c;
                        }
                        else if (comingFrom == SpecializedSlot.trade)
                        {

                            break;
                        }
                    }
                    break;
                case SpecializedSlot.leggings:
                    if (dragItem.item is Leggings l)
                    {
                        if (comingFrom == SpecializedSlot.inventory)
                        {
                            Player.inventory[origin] = new Item(1, Player.leggings);
                            Player.leggings = l;
                        }
                        else if (comingFrom == SpecializedSlot.trade)
                        {

                            break;
                        }
                    }
                    break;
                case SpecializedSlot.trade:
                    if (comingFrom == SpecializedSlot.inventory)
                    {
                        int cost = 0;
                        if (Trade.canAfford(dragItem, InventoryUI.npc.Money, ref cost))
                        {
                            if (InventoryUI.npc.AddItem(dragItem))
                            {
                                Player.inventory[origin].quantity -= dragItem.quantity;
                                Player.Money += cost;
                                InventoryUI.npc.Money -= cost;
                            }
                        }
                    }
                    break;
            }

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            bool consumed = false;
            if (type == SpecializedSlot.inventory)
            {
                if (Player.inventory[slotNumber].item != null)
                {
                    Player.inventory[slotNumber].item.Use(Player.gameObject, ref consumed);
                    if (consumed)
                    {
                        Player.inventory[slotNumber].quantity--;
                    }
                }
            }
        }
    }
}
