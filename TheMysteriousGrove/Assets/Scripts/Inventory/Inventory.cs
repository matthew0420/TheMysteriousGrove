using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;

    public PlayerMovement playerMovementScript;

    private List<Item> itemList;
  
    private Action<Item> useItemAction;

    public void Start()
    {
        Invoke("findPlayer", 0.1f);
    }

    public void findPlayer()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.WoodenAxeWorld, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.WoodenAxeWorld, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.StickWorld, amount = 5 });
        AddItem(new Item { itemType = Item.ItemType.LogWorld, amount = 1 });
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        } else
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item) {
        if (item.IsStackable())
            {
            Item itemInInventory = null;
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                    {
                        inventoryItem.amount -= item.amount;
                        itemInInventory = inventoryItem;
                    }
                }
            if (itemInInventory != null && itemInInventory.amount <= 0)
                {
                    itemList.Remove(itemInInventory);
                }
            }
        else
            {
                itemList.Remove(item);
            }

            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

    public void UseItem(Item item)
    {
        //if item can be equipped, remove it from the itemlist
        // itemList.Remove(item);
       // playerMovementScript.UseItem(item);
        //useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
