using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{
    // Start is called before the first frame update
    
    new void Start()
    {
        base.Start();
        // boosts.Add(BoostType.Health);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if(hp != 0){
            if(isActive){
                attackInterval += Time.deltaTime;

                if(attackInterval >= attackRate){
                    rb.velocity = new Vector2(0, 0);
                    animator.Play("attack");
                    attackInterval = 0;
                }
                if(!animator.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                    Vector2 distance = new Vector2(PlayerController.instance.GetPosition().x - transform.position.x, PlayerController.instance.GetPosition().y - transform.position.y);
                    direction = distance.x / Mathf.Abs(distance.x);
                    rb.velocity = distance / distance.magnitude * speed;
                }
            }
        }
    }
}
