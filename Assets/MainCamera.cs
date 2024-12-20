using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void End(){
        SceneManager.LoadScene("Ending");
    }

    bool IsAllEnemiesDead(){
        var allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        for(int i = 0; i < allEnemies.Length; i++){
            if(!allEnemies[i].IsDead()){
                return false;
            }
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {

        if(this.IsAllEnemiesDead()){
            this.End();
        }

        float PlayerY = PlayerTransform.position.y;
        if(-230 < PlayerY && PlayerY < 0){
            float brightness = (PlayerY + 230) / 230F;
            Water.color = new Color(brightness, brightness, brightness);
        }


        transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z);
        
        if(Random.Range(0, 50) == 0){
            Instantiate(Bubbles, new Vector3(Random.Range(-8F, 8F) + transform.position.x, -7.6F + transform.position.y, -7.5F), Quaternion.identity);
        }
    }
}