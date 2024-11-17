using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D RB;
    private Animator PlayerAnimator;
    private Vector2 MovementVector;
    [SerializeField] float speed;
    private bool IsDashing;
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }

    private void OnMove(InputValue value){
        MovementVector = value.Get<Vector2>();
    }

    void OnDash(InputValue value){
        if(value.Get<float>() == 1){
            IsDashing = true;            
        }

        if(value.Get<float>() == 0){
            IsDashing = false;;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(MovementVector.x * transform.localScale.x < 0){
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        RB.velocity = MovementVector * speed;
        PlayerAnimator.SetInteger("VerticalDirection", (int)MovementVector.y);

        if(IsDashing){
            RB.velocity = new Vector2(RB.velocity.x * 2, RB.velocity.y);
        }
    }
}
