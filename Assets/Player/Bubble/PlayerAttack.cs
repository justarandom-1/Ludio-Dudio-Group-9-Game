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

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Wall")){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPosition = PlayerController.instance.GetPosition();
        if(time < ChargeTime){
            time += Time.deltaTime;

            float PlayerDirection = PlayerController.instance.GetDirection();
            transform.position = new Vector3(PlayerPosition.x + 1.1F * PlayerDirection, PlayerPosition.y - 0.1F, PlayerPosition.z + 0.01F);

            if(time >= ChargeTime){
                RB.velocity = new Vector2(speed * PlayerDirection, 0);
                GetComponent<Collider2D>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if(Mathf.Abs(transform.position.x - PlayerPosition.x) > 15){
            Destroy(gameObject);
        }
    }
}
