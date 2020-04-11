using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Inventory : MonoBehaviour
{

    private Inventory Inventory;
    private Transform ItemSlotContainer;
    private Transform ItemSlotTemplate;

    //public void Start()
   // {
      //  Invoke("SetInventory", 1f);
    //}
    private void Awake()
    {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.Inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

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
        foreach (Item item in Inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

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
