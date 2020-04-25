using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadScript : MonoBehaviour
{
    private int waitTime;

    public void Update()
    {
        if(waitTime < 50)
        {
            waitTime++;
        }
        if(waitTime >= 50)
        {
            this.gameObject.SetActive(false);
        }
    }
}
