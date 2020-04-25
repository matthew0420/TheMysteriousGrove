using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;

    public PlayerMovement playerMovementScript;

    public bool itemInInventory;

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

    //used to spawn the initial inventory, right now it also adds some items to the inventory at start
    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

       /* AddItem(new Item { itemType = Item.ItemType.WoodenAxeWorld, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.WoodenAxeWorld, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.StickWorld, amount = 5 });
        AddItem(new Item { itemType = Item.ItemType.LogWorld, amount = 1 });
        */
    }

    //how items are normally added to the inventory
    public void AddItem(Item item)
    {
        //checks if item is stackable, and handles items in different ways if they are or are not stackable
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                //checks if the stackable items are already in the inventory, so that it can either make
                //a new stack or add to the existing stack
                //ideally, this will eventually also take into consideration if a stack is too large to hold more 
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            //if an item does not stack or there is no current stack in inventory, add to inventory
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

    public bool CheckItem(Item item)
    {
         itemInInventory = false;
         foreach (Item inventoryItem in itemList)
         { 
             if (inventoryItem.itemType == item.itemType)
             {
                itemInInventory = true;
                return true;
             }
         }
        return false;
    }

    //used to remove items from the inventory
    public void RemoveItem(Item item) {
        if (item.IsStackable())
            {
            Item itemInInventory = null;
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

    //this was how the video i watched said to use an item, but since I wanted to 
    //start off with equipping i am running a function in the player script that 
    // sort of replaces this one
    public void UseItem(Item item)
    {
        //if item can be equipped, remove it from the itemlist
        // itemList.Remove(item);
       // playerMovementScript.UseItem(item);
        //useItemAction(item);
    }

    //gets the itemlist, mostly this is called by other scripts
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
