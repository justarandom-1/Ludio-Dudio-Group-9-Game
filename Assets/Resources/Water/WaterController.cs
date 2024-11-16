using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    private Object Bubbles;
    // Start is called before the first frame update
    void Start()
    {
        Bubbles = Resources.Load<GameObject>("Water/Bubbles");
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 50) == 0){
            Instantiate(Bubbles, new Vector3(Random.Range(-8F, 8F) + transform.position.x, -7.6F + transform.position.y, -7.5F), Quaternion.identity);
        }

    }
}
