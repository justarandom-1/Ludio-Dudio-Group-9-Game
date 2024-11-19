using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Vector3 startPosition;
    protected bool isActive;
    [SerializeField] protected int hp;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float speed;
    [SerializeField] protected int atk;
    [SerializeField] protected float range;
    protected float attackInterval;
    protected float direction;
    protected List<BoostType> boosts;
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        isActive = false;
        attackInterval = 0;
        direction = transform.localScale.x / Mathf.Abs(transform.localScale.x);
        boosts = new List<BoostType>();
    }

    protected void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("PlayerAttack")){
            Debug.Log("HIT");
            hp = Mathf.Max(hp - PlayerController.instance.GetAttackStat(), 0);
            if(hp == 0){
                this.kill();
            }
        }
    }

    protected void Update()
    {
        if(hp != 0){

            if(direction * transform.localScale.x < 0 && hp != 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y - 0.05F, transform.localScale.z);
            }
            if(!isActive && (PlayerController.instance.GetPosition() - transform.position).magnitude < range)
            {
                this.setActive();
            }
            else if(isActive && (PlayerController.instance.GetPosition() - transform.position).magnitude > range)
            {
                this.setInactive();
            }

        }       
        
    }

    protected virtual void setActive(){
        isActive = true;
        animator.Play("active");
    }

    protected virtual void setInactive(){
        isActive = false;
        startPosition = transform.position;
        animator.Play("idle");
        rb.velocity = new Vector2(0, 0);
    }

    protected virtual void kill(){
        animator.Play("death");
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;

        for(int i = 0; i < boosts.Count; i++){
            GiveBoostToPlayer(boosts[i]);
        }
    }

    protected void GiveBoostToPlayer(BoostType boost)
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

    public int GetAttackStat(){
        return atk;
    }

    public bool GetIsDead(){
        return hp == 0;
    }
}
