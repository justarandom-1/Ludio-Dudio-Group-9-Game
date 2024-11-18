using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    public float health = 100f;
    public float damage;
    public int attackDamage = 10;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;
    public bool isDead;
    
    public Transform player;
    public float followSpeed = 2f;
    public abstract BoostType GetBoostType();

    private void Start()
    {
        isDead = false;
       
    }

    // Update is called once per frame
    public void Update()
    {
        if (!isDead)
        { 
            player = GameObject.FindWithTag("Player").transform;
            if (IsPlayerInRange())
            {
                FollowPlayer();
            }
        }
    }
    protected virtual void FollowPlayer()
    {
        if (player == null) return;

        // Move towards the player at follow speed
        float step = followSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);
    }
    
    protected bool IsPlayerInRange()
    {
        if (player == null) return false;

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Return true if the player is within detection range
        return distanceToPlayer <= attackRange;
    }

    // Actual attack implementation (can be overridden by derived classes)
    public virtual void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
            lastAttackTime = Time.time;

        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead && other.gameObject.CompareTag("Player"))
        {
            BoostType boost = GetBoostType(); 
            GiveBoostToPlayer(boost); 
            Debug.Log("Boost applied to player: " + boost);
            Destroy(gameObject);
        }else if (!isDead && other.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            damage = PlayerController.instance.GetAttackStat();
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
            isDead = true;
            Die();
        }
    }
    private void GiveBoostToPlayer(BoostType boost)
    {
        switch (boost)
        {
            case BoostType.Health:
                PlayerController.instance.AddHealth(8);  // Example: Add health boost
                break;
            case BoostType.Speed:
                PlayerController.instance.AddSpeed(3f);  // Example: Add speed boost
                break;
            case BoostType.Attack:
                PlayerController.instance.IncreaseAttack(5);  // Example: Increase attack boost
                break;
            case BoostType.ExtraHealth:
                PlayerController.instance.AddHealth(20);  // Example: Add health boost
                break;
            case BoostType.ExtraSpeed:
                PlayerController.instance.AddSpeed(5f);  // Example: Add speed boost
                break;
            case BoostType.ExtraAttack:
                PlayerController.instance.IncreaseAttack(15);  // Example: Increase attack boost
                break;
        }
    }

    // Enum for different boost types
    public enum BoostType
    {
        Health,
        Speed,
        Attack,
        ExtraHealth,
        ExtraSpeed,
        ExtraAttack
    }

    // Handle enemy death
    protected virtual void Die()
    {
        Debug.Log("Enemy has died.");
       
    }
}