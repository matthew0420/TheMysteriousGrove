using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float playerHorizontal;
    public float reach;
    public GameObject playerObject;

    public Rigidbody2D rb;
    public Animator animator;

    public Vector2 movement;
    public Tilemap currentTileMap;
    public TileBase tile;
    public Tile botTile;
    public bool canHarvest = false;
    public bool harvestUp = false;
    public bool harvestDown = false;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        playerHorizontal = movement.x;
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (playerHorizontal == -1)
        {
            playerObject.transform.localScale = new Vector3(-1, playerObject.transform.localScale.y, playerObject.transform.localScale.z);
        }
        if (playerHorizontal == 1)
        {
            playerObject.transform.localScale = new Vector3(1, playerObject.transform.localScale.y, playerObject.transform.localScale.z);
        }

        if (Input.GetKeyDown("space"))
        {
            canHarvest = true;
            //Harvest();
        }
        if (Input.GetKeyUp("space"))
        {
            canHarvest = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            harvestUp = true;
            //Harvest();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            harvestUp = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            harvestDown = true;
            //Harvest();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            harvestDown = false;
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Forest" && canHarvest == true && this.transform.localScale.x == 1 && harvestUp == false && harvestDown == false)
        {
            Debug.Log(this.transform.position);
            currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            Vector3 worldPoint = new Vector3(this.transform.position.x + reach, this.transform.position.y, this.transform.position.z);
            Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
            tile = currentTileMap.GetTile(intPosition);
            Debug.Log(intPosition);
            Debug.Log("Current tile is: " + tile);
            currentTileMap.SetTile(intPosition, null);
            currentTileMap.RefreshTile(intPosition);
            canHarvest = false;
        }

        if (collision.gameObject.tag == "Forest" && canHarvest == true && this.transform.localScale.x == -1 && harvestUp == false && harvestDown == false)
        {
            Debug.Log(this.transform.position);
            currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            Vector3 worldPoint = new Vector3(this.transform.position.x - reach, this.transform.position.y, this.transform.position.z);
            Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
            tile = currentTileMap.GetTile(intPosition);
            Debug.Log(intPosition);
            Debug.Log("Current tile is: " + tile);
            currentTileMap.SetTile(intPosition, null);
            currentTileMap.RefreshTile(intPosition);
            canHarvest = false;
        }

        if (collision.gameObject.tag == "Forest" && canHarvest == true && harvestUp == true && harvestDown == false)
        {
            Debug.Log(this.transform.position);
            currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            Vector3 worldPoint = new Vector3(this.transform.position.x, this.transform.position.y + reach, this.transform.position.z);
            Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
            tile = currentTileMap.GetTile(intPosition);
            Debug.Log(intPosition);
            Debug.Log("Current tile is: " + tile);
            currentTileMap.SetTile(intPosition, null);
            currentTileMap.RefreshTile(intPosition);
            canHarvest = false;
        }

        if (collision.gameObject.tag == "Forest" && canHarvest == true && harvestUp == false && harvestDown == true)
        {
            Debug.Log(this.transform.position);
            currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            Vector3 worldPoint = new Vector3(this.transform.position.x, this.transform.position.y - reach, this.transform.position.z);
            Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
            tile = currentTileMap.GetTile(intPosition);
            Debug.Log(intPosition);
            Debug.Log("Current tile is: " + tile);
            currentTileMap.SetTile(intPosition, null);
            currentTileMap.RefreshTile(intPosition);
            canHarvest = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Forest" && canHarvest == true && this.transform.localScale.x == 1 && harvestUp == false && harvestDown == false)
        {
            Debug.Log(this.transform.position);
            currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            Vector3 worldPoint = new Vector3(this.transform.position.x + reach, this.transform.position.y, this.transform.position.z);
            Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
            tile = currentTileMap.GetTile(intPosition);
            Debug.Log(intPosition);
            Debug.Log("Current tile is: " + tile);
            currentTileMap.SetTile(intPosition, null);
            currentTileMap.RefreshTile(intPosition);
            canHarvest = false;
        }

        if (collision.gameObject.tag == "Forest" && canHarvest == true && this.transform.localScale.x == -1 && harvestUp == false && harvestDown == false)
        {
            Debug.Log(this.transform.position);
            currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            Vector3 worldPoint = new Vector3(this.transform.position.x - reach, this.transform.position.y, this.transform.position.z);
            Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
            tile = currentTileMap.GetTile(intPosition);
            Debug.Log(intPosition);
            Debug.Log("Current tile is: " + tile);
            currentTileMap.SetTile(intPosition, null);
            currentTileMap.RefreshTile(intPosition);
            canHarvest = false;
        }

        if (collision.gameObject.tag == "Forest" && canHarvest == true && harvestUp == true && harvestDown == false)
        {
            Debug.Log(this.transform.position);
            currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            Vector3 worldPoint = new Vector3(this.transform.position.x, this.transform.position.y + reach, this.transform.position.z);
            Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
            tile = currentTileMap.GetTile(intPosition);
            Debug.Log(intPosition);
            Debug.Log("Current tile is: " + tile);
            currentTileMap.SetTile(intPosition, null);
            currentTileMap.RefreshTile(intPosition);
            canHarvest = false;
        }

        if (collision.gameObject.tag == "Forest" && canHarvest == true && harvestUp == false && harvestDown == true)
        {
            Debug.Log(this.transform.position);
            currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
            Vector3 worldPoint = new Vector3(this.transform.position.x, this.transform.position.y - reach, this.transform.position.z);
            Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
            tile = currentTileMap.GetTile(intPosition);
            Debug.Log(intPosition);
            Debug.Log("Current tile is: " + tile);
            currentTileMap.SetTile(intPosition, null);
            currentTileMap.RefreshTile(intPosition);
            canHarvest = false;
        }
    }
    public void Harvest()
    {
        Debug.Log("Harvest time");
        if (this.transform.localScale.x == 1)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, reach);
            if (hitInfo.collider != null)
            {
                Debug.Log(hitInfo);
                currentTileMap = hitInfo.collider.gameObject.GetComponent<Tilemap>();
                Vector3 worldPoint = hitInfo.transform.position;
                Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
                Debug.Log(intPosition);
                tile = currentTileMap.GetTile(intPosition);
                Debug.Log(currentTileMap);
                Debug.Log("Current tile is: " + tile);
                currentTileMap.SetTile(intPosition, null);
                currentTileMap.RefreshTile(intPosition);
            }
        }
        if (this.transform.localScale.x == -1)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -transform.right, reach);
            if (hitInfo.collider != null)
            {
                Debug.Log(hitInfo.collider.gameObject);
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            }
        }
    }
}
