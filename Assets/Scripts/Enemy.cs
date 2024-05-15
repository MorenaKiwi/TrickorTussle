using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 3.0f;
    private Rigidbody enemyRb;
    public Transform target;
    private Animator animator;
    private int lives = 2;
    private bool playerInAttackRange = false;
    private bool isAttacking = false;
    private GameManager gameManager;
    public GameObject[] boostPrefabs;



    // Start is called before the first frame update
    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();  
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
 
    }

    // Update is called once per frame
    void Update()
    {
       movement();
    
    }



     public void movement(){
        if (target != null)
        {
            // Calculate the direction from the zombie to the player
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // Optional: Prevent the zombie from tilting upwards

            // Rotate the zombie to face the player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            // Move the zombie towards the player
            transform.position += direction.normalized * speed * Time.deltaTime;
            animator.SetBool("IsWalking", direction.magnitude > 0.1f);
        }
     }
     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInAttackRange = true;
            StartAttack();
        }
        else if (collision.gameObject.CompareTag("Weapon"))
        {
            lives--;
            if (lives <= 0)
        {
            DeadZombie();
        }
        }
    }

    private void OnCollisionExit(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        playerInAttackRange = false;
        EndAttack();
    }
}

    private void StartAttack()
{
    if (!isAttacking)
    {
        isAttacking = true;
        animator.SetBool("Attack_b", true);
    }
}

    private void EndAttack()
{
    isAttacking = false;
    animator.SetBool("Attack_b", false);
}

    void DeadZombie()
    {
        if (lives == 0)
        {
            Vector3 zombiePosition = transform.position; // Store the zombie's position
            float spwnY = 1.0f;
            zombiePosition.y = spwnY;

            animator.SetBool("Dead", true);
            Destroy(gameObject, 2.0f);
            gameManager.UpdateScore(5);

            int boostIndex = Random.Range(0, boostPrefabs.Length);
            Quaternion spawnRotation = boostPrefabs[boostIndex].transform.rotation;
            Instantiate(boostPrefabs[boostIndex], zombiePosition, spawnRotation);        
            }
    }
}

