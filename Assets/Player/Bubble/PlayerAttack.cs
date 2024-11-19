using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUbble : MonoBehaviour
{
    private Rigidbody2D RB;
    private float time;
    [SerializeField] float ChargeTime;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        time = 0;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPosition = PlayerController.instance.GetPosition();
        if(time < ChargeTime){
            time += Time.deltaTime;

            if(time >= ChargeTime){

                float Direction = PlayerController.instance.GetDirection();

                Vector2 MovementVector = PlayerController.instance.GetMovementVector();

                float Angle = Mathf.Atan2(1.1F, -0.1F) * -1 + 0.5F * Mathf.PI * Direction;
                
                float PlayerAngle = Mathf.Atan2(MovementVector.x, MovementVector.y) * -1 + 0.5F * Mathf.PI * Direction;

                float offset = Mathf.Sqrt(1.1F * 1.1F + 0.1F * 0.1F);

                transform.position = new Vector3(PlayerPosition.x + offset * Mathf.Cos(Angle + PlayerAngle), PlayerPosition.y + offset * Mathf.Sin(Angle + PlayerAngle), PlayerPosition.z + 0.01F);
                RB.velocity = speed * MovementVector / MovementVector.magnitude; 
                GetComponent<Collider2D>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if(Mathf.Abs(transform.position.x - PlayerPosition.x) > 15){
            Destroy(gameObject);
        }
    }
}
