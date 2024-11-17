using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject Player;
    private Transform PlayerTransform;
    private SpriteRenderer Water;

    private Object Bubbles;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerTransform = Player.GetComponent<Transform>();
        Bubbles = Resources.Load<GameObject>("Water/Bubbles");
        Water = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float PlayerY = PlayerTransform.position.y;
        if(-200 < PlayerY && PlayerY < 0){
            float brightness = (PlayerY + 200) * 0.005F;
            Water.color = new Color(brightness, brightness, brightness);
        }


        transform.position = new Vector3(PlayerTransform.position.x + 3.5F, PlayerTransform.position.y, transform.position.z);
        
        if(Random.Range(0, 50) == 0){
            Instantiate(Bubbles, new Vector3(Random.Range(-8F, 8F) + transform.position.x, -7.6F + transform.position.y, -7.5F), Quaternion.identity);
        }
    }
}