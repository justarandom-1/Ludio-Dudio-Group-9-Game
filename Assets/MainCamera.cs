using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject Player;
    private Transform PlayerTransform;

    private Object Bubbles;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerTransform = Player.GetComponent<Transform>();
        Bubbles = Resources.Load<GameObject>("Water/Bubbles");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PlayerTransform.position.x + 3.5F, PlayerTransform.position.y, transform.position.z);
        
        if(Random.Range(0, 50) == 0){
            Instantiate(Bubbles, new Vector3(Random.Range(-8F, 8F) + transform.position.x, -7.6F + transform.position.y, -7.5F), Quaternion.identity);
        }
    }
}