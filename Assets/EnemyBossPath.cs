using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossPath : MonoBehaviour
{
    // Array of waypoints to walk from one to the next one
    [SerializeField]
    public GameObject[] waypoints;
    
    [SerializeField]
    private float moveSpeed = 2f;

    public int EnemyBossDamage = 40;

    Animator animator;

    // Index of current waypoint from which Enemy walks
    private int waypointIndex = 0;
    private PlayerController playerController;

    // Use this for initialization
	private void Start()
    {
        animator = GetComponent<Animator>();
        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
        // Grab PlayerController Script from Game Object that has the tag "Player"
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    

	// Update is called once per frame
	private void Update()
    {
        // Move Enemy
        Move();
	}

    // Method that actually makes Enemy move
    public void Move()
    {
        // If Enemy didn't reach last waypoint move
        // If enemy reached last waypoint then stop
        if (waypointIndex <= waypoints.Length - 1)
        {
            animator.SetBool("isMoving", true);
            // Debug.Log("Slime is trying to move");
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
                //Debug.Log("Slime is at the end");
            }
        } else {
        // Set Animator Bool
        animator.SetBool("isMoving", false);
        // Destroy Game Object Slime
        Destroy(gameObject);

        // Send Console Msg
        Debug.Log("Enemy Reached The End");
        playerController.TakeDamage(EnemyBossDamage);  
        }
    }
}