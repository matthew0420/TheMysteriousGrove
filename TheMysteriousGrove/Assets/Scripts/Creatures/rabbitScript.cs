using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbitScript : MonoBehaviour
{
    public GameObject playerObject;
    public CreatureSpawner creatureSpawner;

    //stuff for rabbit movement
    public GameObject rabbitObject;
    private float rabbitVelocity = 2f;
    private float latestDirectionChangeTime = -5f;
    public float directionChangeTime = 3f;
    private Vector2 movementDirection;
    public Vector2 movementPerSecond;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rabbitObject = this.gameObject;
        Invoke("FindPlayer", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject != null)
        {
            float dist = Vector2.Distance(playerObject.transform.position, this.transform.position);
            Debug.Log(dist);

            if (dist > 20)
            {
                creatureSpawner.currentCreatures--;
                Destroy(this.gameObject);
            }
        }

        if (movementPerSecond.x > 0f)
        {
            rabbitObject.transform.localScale = new Vector3(-0.5f, rabbitObject.transform.localScale.y, rabbitObject.transform.localScale.z);
        }
        if (movementPerSecond.x < 0f)
        {
            rabbitObject.transform.localScale = new Vector3(0.5f, rabbitObject.transform.localScale.y, rabbitObject.transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        NormalMovement();
    }

    void FindPlayer()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        creatureSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<CreatureSpawner>();
    }

    public void HitRabbit ()
    {

    }
    public void KillRabbit()
    {

    }

    public void NormalMovement()
    {
        //after a certain amount of time, change direction of enemy movement
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            movementPerSecond = movementDirection * rabbitVelocity;
        }

        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        latestDirectionChangeTime = Time.time;
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * rabbitVelocity;
        rabbitObject.transform.localScale = new Vector3(rabbitObject.transform.localScale.x, rabbitObject.transform.localScale.y, rabbitObject.transform.localScale.z);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        latestDirectionChangeTime = Time.time;
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * rabbitVelocity;
        rabbitObject.transform.localScale = new Vector3(rabbitObject.transform.localScale.x, rabbitObject.transform.localScale.y, rabbitObject.transform.localScale.z);
    }

}
