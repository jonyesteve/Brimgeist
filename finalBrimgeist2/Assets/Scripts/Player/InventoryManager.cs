using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] public Stat Strength;
    [SerializeField] public Stat Agility;
    [SerializeField] public Stat Intelligence;
    [SerializeField] public Stat Vitality;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;

    ItemSlot draggedSlot;


    private void Awake()
    {
        Strength = new Stat(5, "Strength");
        Agility = new Stat(5, "Agility");
        Intelligence = new Stat(7, "Intelligence");
        Vitality = new Stat(5, "Vitality");
        statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
        statPanel.UpdateStatValues();

        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;

        inventory.OnPointerEnterEvent += ShowToolTip;
        equipmentPanel.OnPointerEnterEvent += ShowToolTip;

        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }


    private void Equip(ItemSlot itemSlot)
    {
        var equippableItem = itemSlot.Item as EquippableItem;
        if(equippableItem != null)
        {
            Equip(equippableItem);
        }
    }
    private void Unequip(ItemSlot itemSlot)
    {
        var equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
        }
    }

    private void ShowToolTip(ItemSlot itemSlot)
    {
        var equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            itemTooltip.ShowTooltip(equippableItem);
        }
    }
    private void HideTooltip(ItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.icon;
            draggableItem.transform.position = Mouse.current.position.ReadValue();
            draggableItem.enabled = true;
        }
    }
    void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }
    void Drag(ItemSlot itemSlot)
    {
        if(draggableItem.enabled)
            draggableItem.transform.position = Mouse.current.position.ReadValue();

    }
    void Drop(ItemSlot itemSlot)
    {
        if(itemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(itemSlot.Item))
        {
            var dragItem = draggedSlot.Item as EquippableItem;
            var dropItem = itemSlot.Item as EquippableItem;

            if(draggedSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }
            if(itemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.Unequip(this);
            }

            statPanel.UpdateStatValues();

            var draggeditem = draggedSlot.Item;
            draggedSlot.Item = itemSlot.Item;
            itemSlot.Item = draggeditem;
        }
        
    }

    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            if (equipmentPanel.AddItem(item, out EquippableItem previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
                inventory.AddItem(item);
        }
    }
    public void Unequip(EquippableItem item)
    {
        if(!inventory.isFull() && equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
            item.Unequip(this);
            statPanel.UpdateStatValues();
        }
    }

}
