using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArm : MonoBehaviour
{
    public float offsetY = 0f;    
    public float waitTime = 2f;
    private  float countTime = 0f;
    private  Animator animator;
    private Vector2 initialPos;
    private Vector2 attackAniPos;
    private void Start()
    {
        // 獲取物件上的Animator組件
        animator = GetComponent<Animator>();
        animator.Play("wave hand"); 
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            Vector3 Pos = new Vector3(other.gameObject.GetComponent<CapsuleCollider2D>().transform.position.x, 
                                     (other.gameObject.GetComponent<CapsuleCollider2D>().bounds.min.y + offsetY),
                                      other.gameObject.GetComponent<CapsuleCollider2D>().transform.position.z);

            animator.speed = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            Vector3 Pos = new Vector3(other.gameObject.GetComponent<CapsuleCollider2D>().transform.position.x, 
                                     (other.gameObject.GetComponent<CapsuleCollider2D>().bounds.min.y + offsetY),
                                      other.gameObject.GetComponent<CapsuleCollider2D>().transform.position.z);

            // 增加停留的時間
            countTime += Time.deltaTime;

            // 如果停留的時間超過5秒，進行相應的操作
            if (countTime >= waitTime)
            {
                animator.speed = 1;
                animator.Play("attack");
                countTime = 0f;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            animator.speed = 1;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack")&&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) 
            {
                countTime = 0f;
                animator.Play("wave hand");
            }          
        }
    }
}
