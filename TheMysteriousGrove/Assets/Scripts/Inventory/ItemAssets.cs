using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite WoodenAxeSprite;
    public Sprite StickSprite;
    public Sprite LogSprite;

    public Sprite WoodenAxeSpriteOutline;
    public Sprite StickSpriteOutline;
    public Sprite LogSpriteOutline;
}
