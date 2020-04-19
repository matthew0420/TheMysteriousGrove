using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public int treeTileHP = 10;
    public GameObject playerObject;

    public void ChopTree(int hpToRemove)
    {
        treeTileHP = treeTileHP - hpToRemove;
        Debug.Log("Chopped at tree");
        //If the player has an axe out, the tree tile will lose some hp depending on what type of axe is equipped
        //When the tree tile gets to 0 hp or less, we will drop logs for the player and destroy the tree tile
    }

    public void GatherSticks()
    {
        //if the player does not have an axe out, harvest sticks
        //this does not effect the tree's hp, but in turn does not always work, this
        //script will be used to 'attempt' to gather suitable sticks
    }

    // Update is called once per frame
    void Update()
    {
        if(treeTileHP <= 0)
        {
            //drop logs
            //destroy tree tile
        }
    }
}
