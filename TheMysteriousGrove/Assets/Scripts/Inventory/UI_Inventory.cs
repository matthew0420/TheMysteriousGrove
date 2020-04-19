using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
public class UI_Inventory : MonoBehaviour
{

    private Inventory Inventory;
    private Transform ItemSlotContainer;
    private Transform ItemSlotTemplate;
    private PlayerMovement player;

    //finds the two templates that are in the scene, these will be used
    //to generate items when they are added and the inventory itself
    private void Awake()
    {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");
    }

    //finds player prefab
    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    //sets an inventory
    public void SetInventory(Inventory inventory)
    {
        this.Inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        //Updates positions of items in the inventory
        RefreshInventoryItems();
    }

    //when the list items are changed, refresh the Inventory
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }


    private void RefreshInventoryItems()
    {
        foreach(Transform child in ItemSlotContainer)
        {
            if (child == ItemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 130f;

        //runs through every item in the inventory
        foreach (Item item in Inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            //runs from Button_UI, detects if an item in inventory is left clicked and then runs a function
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //use item
                player.UseItem(item);
            };
            //runs from Button_UI, detects if an item in inventory is right clicked and then runs a function
            //these might need to be further abstracted or ran through another function first for reasons such
            //as attempting to use a potion with full health, eat or drink when the meters are full, etc
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //drop item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                Inventory.RemoveItem(item);
                ItemWorld.DropItem(player.gameObject.transform.position, duplicateItem);
            };

            //sets up image and info to be displayed etc for each item
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Item").GetComponent<Image>();
            Image textBackground = itemSlotRectTransform.Find("TextBackground").GetComponent<Image>();
            Text uiText = itemSlotRectTransform.Find("Text").GetComponent<Text>();
            textBackground.gameObject.SetActive(false);
            image.sprite = item.GetSprite();
            if (item.amount > 1)
            {
                textBackground.gameObject.SetActive(true);
                uiText.text = (item.amount).ToString();
            } else
            {
                uiText.text = "";
            }

            x++;
            if (x > 3)
            {
                x = 0;
                y--;
            }
        }
    }
}
