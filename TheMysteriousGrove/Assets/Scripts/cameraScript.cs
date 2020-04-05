using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject playerObject;
    public bool cameraIsSet = false;
    // Update is called once per frame
    void Update()
    {
        if(cameraIsSet == false)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            if(playerObject != null)
            {
                cameraIsSet = true;
            }
        }

        if(cameraIsSet == true)
        { 
            this.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -10);
        }
    }
}
