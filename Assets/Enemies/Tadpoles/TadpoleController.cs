using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleController : Enemy
{

    new void Start()
    {
        base.Start();
        boosts.Add(BoostType.Speed);
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Wall")){
            direction *= -1;
        }
    }

    new void Update()
    {
        base.Update();
        if(hp != 0){
            rb.velocity = new Vector2(direction * speed, 0);
        }
    }
}
