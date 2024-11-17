using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0, 2) == 0){
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Water/BubblesSprite");
        }else{
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Water/BubblesSpriteAlt");
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 6.8F + PlayerController.instance.GetPosition().y){
            Destroy(gameObject);
        }
    }
}
