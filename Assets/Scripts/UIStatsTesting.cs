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
        Stats.text = "Health:"+PlayerController.health.ToString() + 
                     "\nSpeed:"+PlayerController.speed.ToString() +
                     "\nAttack:"+PlayerController.AttackStat.ToString();
    }
}
