using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//影片:https://www.youtube.com/watch?v=e69vNsgUQ_I&list=PLfQU2ch2UpvFO-H2MAwQ2dYekAwQ69IPM&index=11
//用法:新增3個物件作為位置的判斷，然後將範圍設定好後，拖動左右物件到inspector即可
public class Dragon : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public float HearDistance = 5f; //能聽到的距離
    float waitTime;
    public Transform leftDownPos;
    public Transform rightUpPos;//移動的範圍
    public Transform movepos;//移動的位置
    public bool isflat;//是不是平面
    private GameObject Player;  
    bool detected = true;
    public int dmg;

    private Animator animator;
    SpriteRenderer spriteRenderer;
    public float hurtTime=0.01f;//顯示時間
    private float hurtTimer;//計時器
    void Start()
    {
        waitTime = startWaitTime;
        movepos.position = GetRandomPos();//初始在隨機位置
        Player = GameObject.Find("Player");
        hurtTimer = hurtTime;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("isWalk",true);
    }


    void Update()
    {
        //玩家靠近怪物則播放音效
        if(Vector2.Distance(transform.position,Player.transform.position) < HearDistance)
        {
            AudioManager.Instance.PlayMonster("Drogen");      
        }

        if (Vector2.Distance(transform.position, movepos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                movepos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else transform.position = Vector2.MoveTowards(transform.position, movepos.position, speed * Time.deltaTime);
        //平滑地移向目標。用參數控制移動速度

        if (hurtTimer <= 0)
        {
            spriteRenderer.color = Color.white;
        }
        else
            hurtTimer -= Time.deltaTime;
    }

    Vector3 GetRandomPos()//在範圍內獲取隨機位置
    {
        Vector3 rndPos;
        if (isflat)
        {
            rndPos = new Vector3(Random.Range(leftDownPos.position.x, rightUpPos.position.x), transform.position.y, 0);
        }
        else rndPos = new Vector3(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y), 0);

        if (rndPos.x > transform.position.x) transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), 0);
        else transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), 0);
        return rndPos;
    }
    IEnumerator invincibleTime()
    {
        detected = false;
        yield return new WaitForSeconds(0.5f);
        detected = true;
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player" && detected)
        {
            StartCoroutine(invincibleTime());
            //Debug.Log("Player Detected! KILL!!!");
            PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - dmg);
        }
        if (player.gameObject.CompareTag("AttackArea")) 
        {
            spriteRenderer.color = Color.red;
            hurtTimer = hurtTime;
        }
    }
}
