using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidRobot : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float tempSpeed = 2f;
    private Rigidbody2D rb;
    private Animator animator;
    public float HearDistance = 10f; //能聽到的距離
    private bool isGround = false;
    private bool isWall = false;
    private bool isAttack = false;
    private bool isFlip = false;
    public bool isHit = false;
    private GameObject Player;  

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        tempSpeed = moveSpeed;
    }
    private void Update()
    {

        if (isGround)
        {
            if (isAttack)
            {
                //AudioManager.Instance.MonsterSource.Stop();

                if (isWall)
                {
                    moveSpeed = 0f;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, Player.transform.position) < HearDistance)
                {
                    AudioManager.Instance.PlayMonster("RobotWalk");
                }
                if (isWall)
                {
                    transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
                }
            }

            if (FacingLeft())
            {
                rb.velocity = new Vector2(-moveSpeed, 0f);
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, 0f);
            }
        }
        else
        {
            if (!isFlip)
            {
                isFlip = true;
                StartCoroutine(Faling());
            }

        }
        if (isHit)
        {
            AudioManager.Instance.PlayMonster("RobotHit");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGround = true;
        }
        if (other.CompareTag("AttackArea"))// be attacked by player
        {
            isHit = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGround = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isWall = true;
        }
        if (other.CompareTag("Player"))
        {
            transform.localScale = new Vector2(Mathf.Sign(other.transform.position.x - transform.position.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            isAttack = true;
            if (isGround) animator.SetBool("canRun", true);
            moveSpeed = tempSpeed * 3;
        }
    }   
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGround = false;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isWall = false;
        }
        if (other.CompareTag("Player"))
        {
            animator.SetBool("canRun", false);
            moveSpeed = tempSpeed;
            isAttack = false;
        }
        if (other.CompareTag("AttackArea"))
        {
            isHit = false;
        }
    }

    private bool FacingLeft()
    {
        return transform.localScale.x < Mathf.Epsilon;
    }

    IEnumerator Faling()
    {
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        yield return new WaitForSeconds(1f);
        isFlip = false;
    }
}
