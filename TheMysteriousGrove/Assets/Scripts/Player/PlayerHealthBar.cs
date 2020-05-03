using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerMovement playerMovement;
    public Image fillImage;
    public Slider slider;

    void Start()
    {
        Invoke("FindPlayer", 0.2f);
    }

    public void FindPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerMovement = playerObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement != null)
        {
            slider.value = playerMovement.playerHealth;
        }

        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        } else
        {
            fillImage.enabled = true;
        }
    }
}
