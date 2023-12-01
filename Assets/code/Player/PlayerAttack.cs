using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackarea;
    private Animator animator;
    private float attacktimer = 0f;
    public float attackingtime = 0.1f;
    public bool isAttackable;
    public bool attackState = false;

    void Start()
    {
        isAttackable = true;
        attackarea.SetActive(false);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AttackControl();
    }
    private void AttackControl()
    {

        if (Input.GetKeyDown(KeyCode.J) && isAttackable && !(attackState))
        {
            Attack();
        }
        if (attackState)
        {
            attacktimer += Time.deltaTime;

            if (attacktimer >= attackingtime)
            {
                attacktimer = 0;
                attackState = false;
                attackarea.SetActive(attackState);
            }
        }
    }
    private void Attack()
    {
        //播放音效
        AudioManager.Instance.PlayPlayer("Attack");

        // 在動畫播放結束後執行回調函數
        animator.SetBool("CanJump", false);
        animator.SetBool("Attack", true);
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("StopAnimation", animationLength);

        attackState = true;
        attackarea.SetActive(attackState);
    }

    void StopAnimation()
    {
        animator.SetBool("Attack", false);
    }

}
