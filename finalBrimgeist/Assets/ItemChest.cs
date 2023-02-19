using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemChest : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;

    private bool playerInRange;
    private bool isEmpty;
    private bool chestOpened;

    private void Update()
    {
        OpenChest();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        playerInRange = false;
    }

    void OpenChest()
    {
        if(playerInRange && Keyboard.current.eKey.isPressed)
        {
            if (isEmpty == false)
            {
                isEmpty = true;
                inventory.AddItem(item);
            }
        }
    }

}

public class LootboxPanel : MonoBehaviour
{
    [SerializeField] bool tooltipShown;
    [SerializeField] Inventory lootInventory;
    
}
