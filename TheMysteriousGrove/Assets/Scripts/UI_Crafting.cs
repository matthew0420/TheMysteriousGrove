using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Crafting : MonoBehaviour
{


    // public Inventory playerInventory;
    public GameObject playerObject;
    public PlayerMovement playerScript;
    public GameObject woodenAxeRecipe;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FindPlayer", 0.5f);
    }

    public void FindPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearRecipe()
    {
        //should clear all recipes displayed
        woodenAxeRecipe.SetActive(false);
    }

    public void SelectCraftWoodenAxe ()
    {
        woodenAxeRecipe.SetActive(true);
        //display 5 sticks in crafting space
    }

    public void CraftWoodenAxe ()
    {   if (playerScript.inventory.CheckItem(new Item { itemType = Item.ItemType.StickWorld, amount = 5 }) == true)
        {
            playerScript.inventory.AddItem(new Item { itemType = Item.ItemType.WoodenAxeWorld, amount = 1 });
            playerScript.inventory.RemoveItem(new Item { itemType = Item.ItemType.StickWorld, amount = 5 });
        }
        else
        {
            Debug.Log("No sticks in inventory!");
        }
    }

    public void Craft ()
    {
        if (woodenAxeRecipe.activeInHierarchy)
        {
            CraftWoodenAxe();
        }
    }
}
