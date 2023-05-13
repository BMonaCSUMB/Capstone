using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    bool isAlive = true;
    private int EnemySlain;
    public float EnemyHealth = 3;
    public float SlimeXp = 20;
    private LevelingSystem levelingSystem;


    // Start is called before the first frame update
    void Start() {
        // # of Enemies slain is 0
        EnemySlain = 0;
        // Get Component Animator
        animator = GetComponent<Animator>();
        // Set bool isAlive for animator 
        animator.SetBool("isAlive", isAlive);
        //Debug.Log(EnemySlain);
        levelingSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelingSystem>();
    }

    // Health float is called OnHit
    public float Health {
        set {
            isAlive = true;
            // If value is less than current Enemy Health, 
            // enemy is being hit, set trigger
            if(value < EnemyHealth) {
                animator.SetTrigger("hit");
            }

            // Set enemy health to value
            EnemyHealth = value; 

            // Check if enemy health is less than or equal to 0
            // enemy is dead, set bool false
            if(EnemyHealth <= 0) {
                animator.SetBool("isAlive", false);
                EnemySlain += 1;
                //Debug.Log(EnemySlain);
            }
        } get {
            // Get new value of EnemyHealth
            return EnemyHealth;        
        }
    }

    // This is called once per frame
    void Update()
    {
        // If # of Enemies Slain is at least 1 gain Slime Xp
        if(EnemySlain >= 1)
        {
            // Use Gain Exp function from levelingsystem script to gain slime xp
            levelingSystem.GainExp(SlimeXp);
            // Set Enemies slain back to 0
            EnemySlain -= 1;
        }
    }

    // Called when Sword strike
    void OnHit(float damage) {
        //Debug.Log("Slime Hit for " + damage);
        // Subtract damage from Health
        Health -= damage;
    }

    // Used in Animation Event for slime defeated animation
    public void RemoveEnemy() {
        // Destroy the enemy object when called
        Destroy(gameObject);
    }
}

