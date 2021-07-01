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
        inventory, weapon, helmet, chest, leggings

    }
    public SpecializedSlot type;
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
                Debug.Log(icon);
                icon.sprite = Player.weapon?.sprite ?? null;
                break;
            case SpecializedSlot.helmet:
                break;
            case SpecializedSlot.chest:
                break;
            case SpecializedSlot.leggings:
                break;
            default:
                break;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (type == SpecializedSlot.inventory)
        {
            dragItem = Player.inventory[slotNumber];
            origin = slotNumber;
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        item.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        item.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Slot"))
        {
            originSlot.alpha = 1;
            Item temp = Player.inventory[slotNumber];
            switch (type)
            {
                case SpecializedSlot.inventory:
                    Player.inventory[slotNumber] = dragItem;
                    Player.inventory[origin] = temp;
                    Destroy(item.gameObject);
                    break;
                case SpecializedSlot.weapon:
                    if (dragItem.item is Weapon w)
                    {
                        Player.inventory[origin] = null;
                        Player.weapon = w;
                        Destroy(item.gameObject);
                    }
                    else
                    {
                        Destroy(item.gameObject);
                    }
                    break;
                case SpecializedSlot.helmet:
                    break;
                case SpecializedSlot.chest:
                    break;
                case SpecializedSlot.leggings:
                    break;
            }
        }
    }
}
