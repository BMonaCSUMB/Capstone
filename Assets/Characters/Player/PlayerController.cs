using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public GameObject swordHitbox;

    // Has two float variables stored in it, one for X (left/right) one for Y (up/down)
    Vector2 movementInput;
    // Rigidbody component
    Rigidbody2D rb;
    // Animator component
    Animator animator;
    // Sprite Renderer
    SpriteRenderer spriteRenderer;
    // RaycastHit2D information returned about an object that is detected by Raycast in 2d
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); 
    // Collider2D
    Collider2D swordCollider;

    private PauseMenu pauseMenu;
    bool canMove = true;
    public int maxHealth = 100;
    public static int currentHealth;
    public HealthBarScript healthBar;
    public bool thePlayerisDead;
    private EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        // Get Component looks for a type, Rigidbody is the type
        rb = GetComponent<Rigidbody2D>();
        // Get Component looks for a type, Animator is the type
        animator = GetComponent<Animator>();
        // Get Component looks for a type, SpriteRenderer is the type
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Get Component sword collider
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        // Set isAlive animaton bool to true
        animator.SetBool("isAlive", true); 
        // Send Console message that player is alive for testing purposes
        Debug.Log("Player is Alive");
        // Set current health equal to max
        currentHealth = maxHealth;
        // Set MaxHealth to maxHealth, which was just set to current health
        healthBar.setMaxHealth(maxHealth);
        // Grab PauseMenu Script from Game Object that has the tag "Player"
        pauseMenu = GameObject.FindGameObjectWithTag("Player").GetComponent<PauseMenu>();
        // Grab Enemy Spawner Script from Game Object that has the tag "Player"
        enemySpawner = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemySpawner>();
    }

    // Test function
    // public void Test()
    // {
    //     Debug.Log("TEST CONFIRMED");
    // }

    private void FixedUpdate()
    {
        if(canMove) {
            // If movement input is not 0, try to move
            if(movementInput != Vector2.zero) {
                // Normal movement
                bool success = TryMove(movementInput);

                // If fail try x axis
                if(!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                // If fail try y axis
                if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                // Set Animator Bool
                animator.SetBool("isMoving", success);
                } else {
                    // Set Animator Bool
                    animator.SetBool("isMoving", false);
                }

                // Set direction of the sprite to direction of movement
                if (movementInput.x < 0) {
                    // If the movement input on X is negative set sprite flip true
                    spriteRenderer.flipX = true;
                } else if (movementInput.x > 0) {
                    // If the movement input on X is positive set sprite flip false
                    spriteRenderer.flipX = false;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Test function for testing healthbar damage when using spacebar
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TakeDamage(20);
        // }

        // If player is dead disable the playercontroller by setting bool value to true
        if (thePlayerisDead == true)
        {
            // Disable this update function
            enabled = false;
            //Debug.Log("Cannot Send Dmg Anymore To Player");
        }
    }

    // Take Damage Function
    public void TakeDamage(int damage)
    {
        // Subtract damage from currentHealth
        currentHealth -= damage;
        // Call setHealth function from healthBar Script and set as new currentHealth
        healthBar.setHealth(currentHealth);
        // If the currentHealth is less than or equal to 0, which means player is dead
        if( currentHealth <= 0)
        {
            // Call PlayerisDead function from inside PlayerController Script
            PlayerisDead();
            // Set bool to true
            thePlayerisDead = true;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero) {
            // Check for collisions
            int count =  rb.Cast(
                direction,      // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if(count == 0) {
                // Calculation for collision on obstacles
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); 
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if no direction to move in, so return false
            return false;
        }
    }

    // Recieve an argument (x,y direction that player is trying to move)
    void OnMove(InputValue movementValue)
    {
        //Get Vector2 and store it into movementInput
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        // If the game is not paused, don't allow player to attack while paused
        if(pauseMenu.isPaused == false){
            // Set Animator Trigger
            animator.SetTrigger("swordAttack");
        }
    }

    public void swingSword()
    {
        // Prevent player from moving during attack, used in animation
        LockMovement();
        if(spriteRenderer.flipX == true) {
            // Call AttackLeft function from swordAttack Script
            swordAttack.AttackLeft();
        } else {
            // Call AttackRight function from swordAttack Script
            swordAttack.AttackRight();
        }
    }

    // Used in animation for ending animation of sword swing
    public void endSwing()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    // Lock movement
    public void LockMovement()
    {
        canMove = false;
    }

    // Unlock movement 
    public void UnlockMovement()
    {
        canMove = true;
    }

    // If the Player is Dead Function
    public void PlayerisDead()
    {
        LockMovement();
        // Set Animator Bool
        animator.SetBool("isAlive", false);
        // Send Console Msg
        Debug.Log("Player is Dead from Player Controller Script");
        // Set bool to true
        thePlayerisDead = true;
    }
}
