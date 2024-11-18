using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerController instance;
    public static float health = 50f;
    public static float AttackStat = 10f;
    public static float speed = 4f;
    private Rigidbody2D RB;
    private Animator PlayerAnimator;
    private Vector2 MovementVector;
    private AudioSource AS;
    [SerializeField] float DashLimit;
    private bool IsDashHeld;
    private bool IsDashing;
    private float DashTime;
    private float Direction;

    [SerializeField] AudioClip DashSFX;

    [SerializeField] Object Projectile;
    [SerializeField] AudioClip AttackSFX;
    private void Start()
    {
        instance = this;
        RB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();

        Direction = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        DashTime = 0;
        IsDashHeld = false;
        IsDashing = false;
    }

    private void OnMove(InputValue value){
        MovementVector = value.Get<Vector2>();

        if(MovementVector.x != 0){
            Direction = MovementVector.x;
        }
    }

    void OnDash(InputValue value){
        if(value.Get<float>() == 1 && DashTime < DashLimit){
            IsDashHeld = true;      
        }

        if(value.Get<float>() == 0 && IsDashHeld){
            IsDashHeld = false;
        }
    }

    void OnAttack(){
        if(!PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack")){
            AS.PlayOneShot(AttackSFX);
            PlayerAnimator.Play("attack");
            Instantiate(Projectile, new Vector3(transform.position.x + 1.1F * Direction, transform.position.y - 0.1F, transform.position.z + 0.01F), Quaternion.identity);
        }
    }

    public void TakeDamage(float enemyDamage)
    {
        Debug.Log("Player Health: " + health);
        health -= enemyDamage;
        if (health <= 0)
        {
            Debug.Log("Player Died");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemies enemy = other.GetComponent<Enemies>();
            if (enemy != null && enemy.isDead)  
            {
                PlayerAnimator.Play("attack");
            }
        }
    }
    

    // Update is called once per frame
    private void Update()
    {
        if(MovementVector.x * transform.localScale.x < 0){
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y - 0.05F, transform.localScale.z);
        }

        RB.velocity = MovementVector * speed;
        // PlayerAnimator.SetInteger("VerticalDirection", (int)MovementVector.y);

        if(IsDashHeld && !IsDashing && (MovementVector.x != 0 || MovementVector.y != 0)){
            IsDashing = true;
            AS.PlayOneShot(DashSFX);
        }

        if(!IsDashHeld && IsDashing){
            IsDashing = false;
            DashTime = 0;
        }

        if(IsDashing){
            DashTime += Time.deltaTime;
            if(DashTime >= DashLimit){
                IsDashHeld = false;
                IsDashing = false;
                DashTime = 0;
            }else{
                RB.velocity *= 2;
            }
        }
    }
    
    // Method to increase player's health
    public static void AddHealth(float value)
    {
        health += value;
        Debug.Log("Player Health: " + health);
    }

    // Method to increase player's speed
    public static void AddSpeed(float value)
    {
        speed += value;
        Debug.Log("Player Speed: " + speed);
    }

    // Method to increase player's attack power
    public static void IncreaseAttack(float value)
    {
        AttackStat += value;
        Debug.Log("Player Attack: " + AttackStat);
    }

    public Vector3 GetPosition(){
        return transform.position;
    }

    public float GetDirection(){
        return Direction;
    }
    public static float GetAttackStat()
    {
        return AttackStat; 
    }
}
