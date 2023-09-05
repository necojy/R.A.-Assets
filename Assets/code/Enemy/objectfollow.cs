using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//影片:
//用法:拖動主角到Player(inspector)，設定偵測範圍(judge_dis)
public class objectfollow : MonoBehaviour
{
    public float movespeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Transform Player;

    public float judge_dis;
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update() {
        
        float dis = (transform.position - Player.position).sqrMagnitude;
         
        if(dis <= judge_dis)
        {
            transform.position = Vector2.MoveTowards(transform.position,Player.position,movespeed * Time.deltaTime);
        }
        
    }
    
}
