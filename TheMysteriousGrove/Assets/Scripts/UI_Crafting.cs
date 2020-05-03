using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Crafting : MonoBehaviour
{


    // public Inventory playerInventory;
    public GameObject playerObject;
    public PlayerMovement playerScript;
    public GameObject woodenAxeRecipe;
    public GameObject campFireRecipe;

    public GameObject cookedRabbitRecipeIcon;
    public GameObject cookedRabbitRecipe;

    public bool campFireActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FindPlayer", 0.5f);
    }

    private void FindPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<PlayerMovement>();
    }

    public void ClearRecipe()
    {
        //should clear all recipes displayed
        //this is called in the UI button before the recipe is selected
        woodenAxeRecipe.SetActive(false);
        campFireRecipe.SetActive(false);
        cookedRabbitRecipe.SetActive(false);
    }

    public void SelectCraftWoodenAxe ()
    {
        woodenAxeRecipe.SetActive(true);
        //display 5 sticks in crafting space
    }

    public void SelectCampFire()
    {
        campFireRecipe.SetActive(true);
    }

    public void SelectCookRabbit()
    {
        cookedRabbitRecipe.SetActive(true);
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

    public void CraftCampFire()
    {
        if (playerScript.inventory.CheckItem(new Item { itemType = Item.ItemType.LogWorld, amount = 5 }) == true)
        {
            playerScript.inventory.AddItem(new Item { itemType = Item.ItemType.CampFire, amount = 1 });
            playerScript.inventory.RemoveItem(new Item { itemType = Item.ItemType.LogWorld, amount = 5 });
        }
        else
        {
            Debug.Log("No sticks in inventory!");
        }
    }

    public void CookRabbit()
    {
        if (playerScript.inventory.CheckItem(new Item { itemType = Item.ItemType.UncookedRabbit, amount = 1 }) == true)
        {
            playerScript.inventory.AddItem(new Item { itemType = Item.ItemType.CookedRabbit, amount = 1 });
            playerScript.inventory.RemoveItem(new Item { itemType = Item.ItemType.UncookedRabbit, amount = 1 });
        }
        else
        {
            Debug.Log("No uncooked rabbit in inventory!");
        }
    }

    public void Craft ()
    {
        if (woodenAxeRecipe.activeInHierarchy)
        {
            CraftWoodenAxe();
        }
        if (campFireRecipe.activeInHierarchy)
        {
            CraftCampFire();
        }
        if (cookedRabbitRecipe.activeInHierarchy)
        {
            CookRabbit();
        }
    }
}
