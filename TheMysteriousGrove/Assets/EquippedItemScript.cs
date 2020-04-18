using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemScript : MonoBehaviour
{
    public GameObject playerGameObject;
    public PlayerMovement playerMovementScript;
    public bool moveRight;

    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerMovementScript = playerGameObject.GetComponent<PlayerMovement>();
    }
}
