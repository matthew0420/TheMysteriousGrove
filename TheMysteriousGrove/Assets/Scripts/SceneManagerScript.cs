using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject playerObject;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FindPlayer", 0.1f);
    }

    private void FindPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerMovement = playerObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(playerObject != null && playerMovement.playerHealth <= 0f)
        {
            SceneManager.LoadScene("DeathMenu");
        }
    }

    public void RestartGame()
    {
        //play sound
        Invoke("LoadGame", 0.5f);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
