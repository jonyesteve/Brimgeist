using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;
public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Item item;
    
    [SerializeField] Image image;
    Color normalColor = Color.white;
    Color disabledColor = new Color(1,1,1,0);

    [SerializeField] ItemTooltip tooltip;
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    public Item Item
    {
        get => item;
        set
        {
            item = value;
            if (item == null)
            {
                image.color = disabledColor;
            }
            else
            {
                image.sprite = item.icon;
                image.color = normalColor;
            }
        }
    }


    protected virtual void OnValidate()
    {
        if (image == null) image = GetComponent<Image>();
    }


    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }



    Vector2 originalPos;
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }
}
