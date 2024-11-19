using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerController instance;
    [SerializeField] int health;
    [SerializeField] int AttackStat;
    [SerializeField] float speed;
    private Rigidbody2D RB;
    private SpriteRenderer spriteRenderer;
    private Animator PlayerAnimator;
    private Vector2 MovementVector;
    private AudioSource AS;
    [SerializeField] float DashLimit;
    private bool IsDashHeld;
    private bool IsDashing;
    private float DashTime;
    private float Direction;
    private bool isAtMouse;

    [SerializeField] AudioClip DashSFX;

    [SerializeField] Object Projectile;
    [SerializeField] AudioClip AttackSFX;
    protected PlayerInput input;
    private void Start()
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();

        Direction = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        DashTime = 0;
        IsDashHeld = false;
        IsDashing = false;
        input = GetComponent<PlayerInput>();

        isAtMouse = false;

        MovementVector = new Vector2(0, 0);
    }

    void OnMouseOver()
	{
        isAtMouse = true;
	}

    void OnMouseExit()
	{
        isAtMouse = false;
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
            AS.PlayOneShot(AttackSFX, 0.5F);
            PlayerAnimator.Play("attack");
            Instantiate(Projectile, new Vector3(transform.position.x + 1.1F * Direction, transform.position.y - 0.1F, transform.position.z + 0.01F), Quaternion.identity);
        }
    }

    public void TakeDamage(int enemyDamage)
    {
        Debug.Log("Player Health: " + health);
        health = Mathf.Max(health - enemyDamage, 0);
        if (health == 0)
        {
            this.Kill();
        }
    }


    public void Kill()
    {
        RB.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
        input.enabled = false;
        PlayerAnimator.Play("PlayerDeath");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            this.TakeDamage(other.gameObject.transform.parent.gameObject.GetComponent<Enemy>().GetAttackStat());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemies enemy = other.gameObject.GetComponent<Enemies>();
            if (enemy != null && enemy.isDead)  
            {
                PlayerAnimator.Play("attack");
            }
        }
    }
    

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log(MovementVector);
        if(!isAtMouse){
            MovementVector = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

            Direction = MovementVector.x / Mathf.Abs(MovementVector.x);

            float angle = Mathf.Atan2(MovementVector.x, MovementVector.y) * Mathf.Rad2Deg * -1 + 90 * Direction;

            transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));

            if(Direction * transform.localScale.x < 0){
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y - 0.05F, transform.localScale.z);
            }

            RB.velocity = MovementVector / MovementVector.magnitude * speed;
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

        if(health == 0 && !spriteRenderer.enabled){
            SceneManager.LoadScene("GameOver");
        }
    }
    
    // Method to increase player's health
    public void AddHealth(int value)
    {
        health += value;
        Debug.Log("Player Health: " + health);
    }

    // Method to increase player's speed
    public void AddSpeed(float value)
    {
        speed += value;
        Debug.Log("Player Speed: " + speed);
    }

    // Method to increase player's attack power
    public void IncreaseAttack(int value)
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

    public Vector2 GetMovementVector(){
        return MovementVector;
    }
    public int GetAttackStat()
    {
        return AttackStat; 
    }
    public int GetHealth()
    {
        return health; 
    }
    public float GetSpeed()
    {
        return speed; 
    }
}
