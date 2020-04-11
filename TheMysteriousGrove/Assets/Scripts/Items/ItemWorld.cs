using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWorld : MonoBehaviour
{
    
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    public Text itemText;
    public Image itemTextBackground;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //itemText = transform.FindChild("itemText").GetComponent<Text>();
        //itemTextBackground = transform.FindChild("textBackground").GetComponent<Image>();
        this.itemTextBackground.gameObject.SetActive(false);
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            this.itemTextBackground.gameObject.SetActive(true);
            this.itemText.text = item.amount.ToString();
        } else
        {
            this.itemTextBackground.gameObject.SetActive(false);
            this.itemText.text = "";
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}


