using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingItem : MonoBehaviour
{
    public GameObject playerGameObject;
    public PlayerMovement playerMovementScript;
    public GameObject canvas;
    public GameObject craftingObject;
    public UI_Crafting crafting;

    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerMovementScript = playerGameObject.GetComponent<PlayerMovement>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        craftingObject = canvas.transform.Find("UI_Crafting").gameObject;
        crafting = craftingObject.GetComponent<UI_Crafting>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.gameObject.tag == "Player")
        {
            crafting.cookedRabbitRecipeIcon.SetActive(true);
            crafting.campFireActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            crafting.cookedRabbitRecipeIcon.SetActive(false);
            crafting.cookedRabbitRecipe.SetActive(false);
            crafting.campFireActive = false;
        }
    }
}
