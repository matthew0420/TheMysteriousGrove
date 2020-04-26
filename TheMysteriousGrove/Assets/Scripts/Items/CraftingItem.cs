using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingItem : MonoBehaviour
{
    public GameObject playerGameObject;
    public PlayerMovement playerMovementScript;

    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerMovementScript = playerGameObject.GetComponent<PlayerMovement>();
    }
}
