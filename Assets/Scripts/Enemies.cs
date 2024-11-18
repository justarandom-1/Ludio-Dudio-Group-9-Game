using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float health = 100f;
    public float damage;
    public float attackDamage = 10f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;
    
    public Transform player;
    
    // Update is called once per frame
    public void Update()
    {
        Attack();
    }

    // Handle enemy attack logic
    public void Attack()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            // Perform attack
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    // Actual attack implementation (can be overridden by derived classes)
    protected virtual void PerformAttack()
    {
        Debug.Log("Enemy attacks for " + attackDamage + " damage!");
        // player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            damage = PlayerController.GetAttackStat();
            TakeDamage(damage);
            
        }
    }
    // Call this method to apply damage to the enemy
    public void TakeDamage(float playerDamage)
    {
        Debug.Log("Enemy Health: " + health);
        health -= playerDamage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Handle enemy death
    protected virtual void Die()
    {
        Debug.Log("Enemy has died.");
        Destroy(gameObject); // Destroy the enemy object on death
    }
}