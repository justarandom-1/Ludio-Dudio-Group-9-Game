using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatsTesting : MonoBehaviour
{
    public TMP_Text Stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Stats.text = "Health:"+PlayerController.instance.GetHealth().ToString() + 
                     "\nSpeed:"+PlayerController.instance.GetSpeed().ToString() +
                     "\nAttack:"+PlayerController.instance.GetAttackStat().ToString();
    }
}
