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

    public bool inventoryOpen = false;
    public GameObject InventoryObject;

    public GameObject equippedTool;
    public GameObject equippedWoodenAxe;
    public bool itemIsEquipped = false;

    public Item currentlyEquipped;

    public bool moveRight = true;
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    public void Start()
    {
        InventoryObject = GameObject.FindGameObjectWithTag("UI_Inventory");
        InventoryObject.SetActive(false);
        // uiInventory = (UI_Inventory)FindObjectOfType(typeof(UI_Inventory));
    }

    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory = (UI_Inventory)FindObjectOfType(typeof(UI_Inventory));
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(0, 0), new Item { itemType = Item.ItemType.LogWorld, amount = 2 });
        ItemWorld.SpawnItemWorld(new Vector3(3, 4), new Item { itemType = Item.ItemType.StickWorld, amount = 5 });
        ItemWorld.SpawnItemWorld(new Vector3(-2, -2), new Item { itemType = Item.ItemType.WoodenAxeWorld, amount = 1 });
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //Touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            //ADD ITEMS HERE AND SWORD LOGIC OR WEAPON EQUIPS
            //if item is a consumable, run a function to consume item
            //if item is a weapon or tool, run a function to equip it
            case Item.ItemType.WoodenAxeWorld:
                if (itemIsEquipped == false)
                {
                    Debug.Log("Equip Item");
                    EquipItem(item);
                    inventory.RemoveItem(item);
                }
                break;
        }
    }

    public void EquipItem(Item item)
    {
            switch (item.itemType)
            {
                case Item.ItemType.WoodenAxeWorld:
                    if (moveRight == true)
                    {
                        equippedTool = Instantiate(equippedWoodenAxe, new Vector3(playerObject.transform.position.x + 0.3f, playerObject.transform.position.y, playerObject.transform.position.z), Quaternion.identity);
                    }
                    if (moveRight == false)
                    {
                        equippedTool = Instantiate(equippedWoodenAxe, new Vector3(playerObject.transform.position.x - 0.3f, playerObject.transform.position.y, playerObject.transform.position.z), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                    }
                    this.equippedTool.transform.parent = playerObject.transform;
                    itemIsEquipped = true;
                    break;    
        }
    }

    public void UnequipItem()
    {
        if (itemIsEquipped == true)
        {
            if (equippedTool.tag == "WoodenAxe")
            {
                inventory.AddItem(new Item { itemType = Item.ItemType.WoodenAxeWorld, amount = 1 });
                Destroy(equippedTool);
                itemIsEquipped = false;
            }
        }
    }


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
            moveRight = false;
            playerObject.transform.localScale = new Vector3(-1, playerObject.transform.localScale.y, playerObject.transform.localScale.z);
        }
        if (playerHorizontal == 1)
        {
            moveRight = true;
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

        if (Input.GetKeyDown("tab"))
        {
            if(inventoryOpen == true)
            {
                InventoryObject.SetActive(false);
                inventoryOpen = false;
                return;
            }

            if(inventoryOpen == false)
            {
                InventoryObject.SetActive(true);
                inventoryOpen = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UnequipItem();
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
