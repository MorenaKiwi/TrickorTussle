using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 10.0f;
    public float rotationSpeed;
    private Animator animator;
    public HealthBar healthBar;
    public int maxHealth = 10;
    public int currentHealth;
    private GameManager gameManager;
    private bool isAlive = true;
    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticale;
    private Pumpkin pumpkin;

    public float obstacleCheckDistance = 0.10f;
    public LayerMask obstacleLayer;

    // Ground check variables
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    public float jumpForce = 10.0f;
    public float gravityModifier = 2.0f; // Experiment with different values
    public GameObject flashPanel;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        pumpkin = GameObject.Find("Pumpkin").GetComponent<Pumpkin>();
        obstacleLayer = LayerMask.GetMask("Obstacles");
        groundCheck = transform.Find("GroundCheck");

        // Apply gravity modifier
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            playerMovement();
            Jump();
        }
    }

    void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        // Check for collisions before moving
        if (!CheckForObstacleCollision(movementDirection))
        {
            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

            if (movementDirection != Vector3.zero)
            {
                dirtParticle.Play();
                animator.SetBool("IsMoving", true);
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                dirtParticle.Stop();
                animator.SetBool("IsMoving", false);
            }
        }

        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, LayerMask.GetMask("Ground"));
    }

    void Jump()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        if (isGrounded) //if able- edit to be able to jump onto objects
        {
            animator.SetTrigger("Jump");

            playerRb.velocity = new Vector3(playerRb.velocity.x, 0.0f, playerRb.velocity.z); // Reset vertical velocity
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}


    bool CheckForObstacleCollision(Vector3 movementDirection)
    {
        // Perform a sphere cast to check for obstacles in the movement direction
        if (Physics.CheckSphere(transform.position + movementDirection.normalized * obstacleCheckDistance, groundCheckRadius, obstacleLayer))
        {
            // If an obstacle is detected, stop the player's movement
            playerRb.velocity = Vector3.zero;
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            TakeDamage(1);
            FlashPanel();
            if (currentHealth <= 0)
            {
                isAlive = false;
                explosionParticale.Play(); // Start the particle effect

                Invoke("StopExplosionParticle", 1.0f);

                animator.SetTrigger("Dead");
                gameManager.GameOver();
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Stop the player's movement when colliding with an obstacle
            playerRb.velocity = Vector3.zero;
        }
    }
    void FlashPanel()
{
    StartCoroutine(FlashPanelCoroutine());
}

IEnumerator FlashPanelCoroutine()
{
    flashPanel.SetActive(true);
    yield return new WaitForSeconds(2.0f);
    flashPanel.SetActive(false);
}

void StopExplosionParticle()
{
    explosionParticale.Stop();
}


void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
