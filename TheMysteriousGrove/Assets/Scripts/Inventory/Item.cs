using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Stick,
        StickWorld,
        Log,
        LogWorld,
        Stone,
        WoodenAxe,
        WoodenAxeWorld,

    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.WoodenAxe: return ItemAssets.Instance.WoodenAxeSprite;
            case ItemType.Stick:     return ItemAssets.Instance.StickSprite;
            case ItemType.Log:       return ItemAssets.Instance.LogSprite;

            case ItemType.WoodenAxeWorld: return ItemAssets.Instance.WoodenAxeSpriteOutline;
            case ItemType.StickWorld: return ItemAssets.Instance.StickSpriteOutline;
            case ItemType.LogWorld: return ItemAssets.Instance.LogSpriteOutline;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.LogWorld:
            case ItemType.StickWorld:
                return true;
            case ItemType.WoodenAxeWorld:
                return false;
        }
    }

}
