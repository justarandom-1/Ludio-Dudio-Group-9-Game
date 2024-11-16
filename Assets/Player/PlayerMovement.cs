using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D RB;
    private Vector2 MovementVector;
    [SerializeField] float speed;
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value){
        MovementVector = value.Get<Vector2>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(MovementVector.x * transform.localScale.x < 0){
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        RB.velocity = MovementVector * speed;
    }
}