using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    //spawner stuff
    public GameObject creatureSpawner;
    public CreatureSpawner creatureSpawnerScript;

    //stuff for the player's parameters
    public float moveSpeed = 5f;
    public float playerHorizontal;
    public float reach = 1f;
    public bool moveRight = true;
    public bool canHarvest = false;
    public GameObject playerObject;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;

    public float playerHealth = 100f;
    public float playerHunger = 100f;
    public float playerThirst = 100f;

    //audio stuff
    public AudioClip TreeChop;
    public AudioClip TreeFall;
    public AudioClip GatherSticks;
    public AudioClip Eat;
    public AudioClip Drink;
    public AudioClip HitRabbit;
    public AudioSource PlayerAudio;

    //tile stuff
    Vector3 worldPoint;
    public Tilemap currentTileMap;
    public TileBase tile;
    public Tile botTile;
    RaycastHit2D collision;
    public string rayCastDirection;

    //menus and storage stuff
    public bool inventoryOpen = false;
    public bool craftingOpen = false;
    public bool itemIsEquipped = false;
    public GameObject InventoryObject;
    public GameObject CraftingObject;
    public GameObject equippedTool;
    public GameObject equippedWoodenAxe;
    public GameObject campFireCraftStation;
    public Item currentlyEquipped;
    [SerializeField] private UI_Inventory uiInventory;
    public Inventory inventory;

    public void Start()
    {
        InventoryObject = GameObject.FindGameObjectWithTag("UI_Inventory");
        InventoryObject.SetActive(false);

        CraftingObject = GameObject.FindGameObjectWithTag("UI_Crafting");
        CraftingObject.SetActive(false);

        creatureSpawner = GameObject.FindGameObjectWithTag("Spawner");
        creatureSpawnerScript = creatureSpawner.GetComponent<CreatureSpawner>();
    }

    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory = (UI_Inventory)FindObjectOfType(typeof(UI_Inventory));
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

        //How to spawn stuff in the world
        //ItemWorld.SpawnItemWorld(new Vector3(0, 0), new Item { itemType = Item.ItemType.LogWorld, amount = 2 });
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
            case Item.ItemType.WoodenAxeWorld:
                if (itemIsEquipped == false)
                {
                    EquipItem(item);
                    inventory.RemoveItem(item);
                }
                break;
            case Item.ItemType.CampFire:
                    EquipItem(item);
                    inventory.RemoveItem(item);
                    break;
            case Item.ItemType.CookedRabbit:
                PlayerAudio.clip = Eat;
                PlayerAudio.Play();
                playerHunger = playerHunger + 10f;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.CookedRabbit, amount = 1 });
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
            case Item.ItemType.CampFire:
                    Instantiate(campFireCraftStation, new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z), Quaternion.identity);
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

        if (Input.GetKeyUp("space"))
        {
            canHarvest = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rayCastDirection = "up";
        }

        if (Input.GetKey(KeyCode.A))
        {
            rayCastDirection = "left";
        }

        if (Input.GetKey(KeyCode.S))
        {
            rayCastDirection = "down";
        }

        if (Input.GetKey(KeyCode.D))
        {
            rayCastDirection = "right";
        }

        if (Input.GetKeyDown("space"))
        {
            canHarvest = true;
        }

        if (Input.GetKeyDown("tab"))
        {
            if (inventoryOpen == true)
            {
                if (CraftingObject.activeInHierarchy == true)
                {
                    CraftingObject.SetActive(false);
                    craftingOpen = false;
                }
                InventoryObject.SetActive(false);
                inventoryOpen = false;
                return;
            }

            if (inventoryOpen == false)
            {
                if (CraftingObject.activeInHierarchy == true)
                {
                    CraftingObject.SetActive(false);
                    craftingOpen = false;
                }
                InventoryObject.SetActive(true);
                inventoryOpen = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (craftingOpen == true)
            {
                if (InventoryObject.activeInHierarchy == true)
                {
                    InventoryObject.SetActive(false);
                    inventoryOpen = false;
                }
                CraftingObject.SetActive(false);
                craftingOpen = false;
                return;
            }

            if (craftingOpen == false)
            {
                if (InventoryObject.activeInHierarchy == true)
                {
                    InventoryObject.SetActive(false);
                    inventoryOpen = false;
                }
                CraftingObject.SetActive(true);
                craftingOpen = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UnequipItem();
        }

        if (canHarvest == true)
        {
            if (rayCastDirection == "up")
            {
                collision = Physics2D.Raycast(transform.position, new Vector2(0f, 0.02f));
            }
            if (rayCastDirection == "down")
            {
                collision = Physics2D.Raycast(transform.position, new Vector2(0f, -0.02f));
            }
            if (rayCastDirection == "right")
            {
                collision = Physics2D.Raycast(transform.position, new Vector2(0.02f, 0f));
            }
            if (rayCastDirection == "left")
            {
                collision = Physics2D.Raycast(transform.position, new Vector2(-0.02f, 0f));
            }

            if (collision.collider != null)
            {
                if (rayCastDirection == "up")
                {
                    worldPoint = new Vector3(this.transform.position.x, this.transform.position.y + reach, this.transform.position.z);
                }
                if (rayCastDirection == "down")
                {
                    worldPoint = new Vector3(this.transform.position.x, this.transform.position.y - reach, this.transform.position.z);
                }
                if (rayCastDirection == "right")
                {
                    worldPoint = new Vector3(this.transform.position.x + reach, this.transform.position.y, this.transform.position.z);
                }
                if (rayCastDirection == "left")
                {
                    worldPoint = new Vector3(this.transform.position.x - reach, this.transform.position.y, this.transform.position.z);
                }

                if (collision.collider.gameObject.tag == "Forest")
                {
                    currentTileMap = collision.collider.gameObject.GetComponent<Tilemap>();
                    Vector3Int intPosition = currentTileMap.WorldToCell(worldPoint);
                    tile = currentTileMap.GetTile(intPosition);

                    if (tile != null)
                    {
                        foreach (Transform child in this.transform)
                        {
                            if (child.CompareTag("WoodenAxe"))
                            {
                                collision.collider.gameObject.GetComponent<TreeScript>().ChopTree(2);
                                PlayerAudio.clip = TreeChop;
                                PlayerAudio.Play();
                            }
                        }

                        if (this.gameObject.transform.childCount == 0)
                        {
                            inventory.AddItem(new Item { itemType = Item.ItemType.StickWorld, amount = 1 });
                            PlayerAudio.clip = GatherSticks;
                            PlayerAudio.Play();
                        }

                        if (collision.collider.gameObject.GetComponent<TreeScript>().treeTileHP <= 0)
                        {
                            currentTileMap.SetTile(intPosition, null);
                            currentTileMap.RefreshTile(intPosition);
                            PlayerAudio.clip = TreeFall;
                            PlayerAudio.Play();
                            inventory.AddItem(new Item { itemType = Item.ItemType.LogWorld, amount = 3 });
                            collision.collider.gameObject.GetComponent<TreeScript>().treeTileHP = 10;
                        }
                    }
                }

                if (collision.collider.gameObject.tag == "Rabbit" && collision.collider.isTrigger)
                {
                    Debug.Log("Hit rabbit");
                    foreach (Transform child in this.transform)
                    {
                        if (child.CompareTag("WoodenAxe"))
                        {
                            collision.collider.gameObject.GetComponent<rabbitScript>().HitRabbit(1);
                            PlayerAudio.clip = HitRabbit;
                            PlayerAudio.Play();
                        }

                        if (collision.collider.gameObject.GetComponent<rabbitScript>().rabbitHP <= 0)
                        {
                            inventory.AddItem(new Item { itemType = Item.ItemType.UncookedRabbit, amount = 1 });
                            creatureSpawnerScript.currentCreatures--;
                            Destroy(collision.collider.gameObject);
                        }
                    }
                }

                if (collision.collider.gameObject.tag == "Water")
                {
                    PlayerAudio.clip = Drink;
                    PlayerAudio.Play();
                    playerThirst = playerThirst + 5f;
                }
            }
            canHarvest = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        NourishmentScript();

        if (playerThirst <= 0f)
        {
            Thirsty();
        }
        if (playerHunger <= 0f)
        {
            Hungry();
        }

        if (playerHunger >= 75f && playerHealth < 100f)
        {
            PlayerRecover();
        }
        if (playerThirst >= 75f && playerHealth < 100f)
        {
            PlayerRecover();
        }
    }

    public void NourishmentScript()
    {
        //depletes hunger and thirst meters if they are not 0 already
        if (playerThirst > 0f)
        {
            //0.3f
            playerThirst = playerThirst - (0.6f * Time.fixedDeltaTime);
        }
        if (playerHunger > 0f)
        {
            //0.2f
            playerHunger = playerHunger - (0.4f * Time.fixedDeltaTime);
        }

        //prevents meters from going too low or too high
        if (playerThirst < 0f)
        {
            playerThirst = 0f;
        }
        if (playerThirst > 100f)
        {
            playerThirst = 100f;
        }

        if (playerHunger < 0f)
        {
            playerHunger = 0f;
        }
        if (playerHunger > 100f)
        {
            playerHunger = 100f;
        }

        if(playerHealth > 100f)
        {
            playerHealth = 100f;
        }
    }

    public void Thirsty()
    {
        playerHealth = playerHealth - (1f * Time.fixedDeltaTime);
    }
    public void Hungry()
    {
        playerHealth = playerHealth - (1f * Time.fixedDeltaTime);
    }

    public void PlayerRecover()
    {
        playerHealth = playerHealth + (0.4f * Time.fixedDeltaTime);
    }
}
