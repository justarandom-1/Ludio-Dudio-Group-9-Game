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
            GameObject generatedBubbles = Instantiate(Bubbles, new Vector3(Random.Range(-8F, 8F), -7.6F, -7.5F), Quaternion.identity) as GameObject;
        }

    }
}
