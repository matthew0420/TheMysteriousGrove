using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    //holds the 'assets' for the items, 
    //in this case its holding a transform and all of the possible sprite
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
    public Sprite CampFireSprite;
}
