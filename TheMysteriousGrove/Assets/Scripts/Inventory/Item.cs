using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    //defines item types
    public enum ItemType
    {
        Stick,
        StickWorld,
        Log,
        LogWorld,
        Stone,
        WoodenAxe,
        WoodenAxeWorld,
        CampFire,
        UncookedRabbit,
        CookedRabbit
    }

    public ItemType itemType;
    public int amount;

    //gets sprite for each possible item type
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
            case ItemType.CampFire: return ItemAssets.Instance.CampFireSprite;
            case ItemType.UncookedRabbit: return ItemAssets.Instance.UncookedRabbitSprite;
            case ItemType.CookedRabbit: return ItemAssets.Instance.CookedRabbitSprite;
        }
    }

    //differentiates between items that can be stacked and those that cannot stack
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.LogWorld:
            case ItemType.StickWorld:
            case ItemType.UncookedRabbit:
            case ItemType.CookedRabbit:
                return true;
            case ItemType.WoodenAxeWorld:
            case ItemType.CampFire:
                return false;
        }
    }

}
