using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticArm : MonoBehaviour
{
    public float waitForAttack = 0.5f;
    private  Animator animator;
    private bool canAttack = true;
    private void Start()
    {
        // 獲取物件上的Animator組件
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(canAttack)
            {
                StartCoroutine(Attack());
            }
        }        
    }

    IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(waitForAttack); //等待waitForAttack後攻擊
        animator.SetBool("canAttack",true);
        yield return new WaitForSeconds(0.3f); //播放動畫時間
        animator.SetBool("canAttack",false);
        canAttack = true;
    }
}
