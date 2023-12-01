using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider2D;
    BoxCollider2D feet;
    GameObject currentOneWayPlatform;
    bool isright = true;
    public bool isGround;

    public PhysicsMaterial2D airMaterial;
    public PhysicsMaterial2D groundMaterial;
    public float speed;
    public float airspeed;
    public float jumpforce;
    public float doubleJumpSpeed;
    public float sprintSpeed;
    public float sprintTime;
    public bool jumpInput;
    public bool jumpInputRelease;
    public bool isInputEnabled;
    public bool isSprintReset;
    public bool isSprintable;
    public bool invincible;
    public float invincibleTime = 1f;//顯示時間
    public float invincibleTimer;//計時器

    public float crushBack = 8f;
    private Animator animator;

    public float repel = 5f;
    public bool canMove = true;
    private bool canJump = true;
    private bool canDoubleJump;
    public bool doubleJumpSkill;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        isInputEnabled = true;
        isSprintReset = true;
        isSprintable = true;
    }

    private void Update()
    {
        if (isInputEnabled)
        {
            if(canMove) Movement();
            if(canJump) JumpControl();
            OneWayFlatformCheck();
            SprintControl();
        }
        GroundCheck();
        doubleJumpSkill = intToBool(PlayerPrefs.GetInt("doubleJumpSkill", 0));
    }

    private void FixedUpdate() 
    {
        InvincibleCheck();
    }
    private void GroundCheck()
    {
        isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isGround)
            rb.sharedMaterial = groundMaterial;
        else
            rb.sharedMaterial = airMaterial;
    }
    private void OneWayFlatformCheck()
    {
        float moveDeltay = Input.GetAxis("Vertical");
        if (moveDeltay < -0.1f)
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }

    }
    void InvincibleCheck()
    {
        if (invincibleTimer <= 0 && !invincible) {
            Physics2D.IgnoreLayerCollision(7, 6, false);
            Physics2D.IgnoreLayerCollision(7, 8, false);
        }
        else if(invincibleTimer <= 0)
        {
            invincibleTimer = invincibleTime;
            PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - 1);
        }
        else
        {
            invincibleTimer -= Time.deltaTime;
        }
    }
    private void JumpControl()
    {
        jumpInput = Input.GetButtonDown("Jump");
        jumpInputRelease = Input.GetButtonUp("Jump");
        Jump();

    }
    private void SprintControl()
    {
        if (Input.GetKeyDown(KeyCode.K) && isSprintable && isSprintReset)
        {
            Sprint();
        }

    }


    private void Movement()
    {
        float moveDelta = Input.GetAxis("Horizontal");
        //swap direction
        if (moveDelta != 0 && isGround)
        {
            animator.SetBool("isWalking", true);
            AudioManager.Instance.PlayPlayer("Walk");
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if ((moveDelta > 0 && !isright) || (moveDelta < 0 && isright))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isright = !isright;
        }
        if (moveDelta != 0)
        {
            //movement
            if (isGround)
                rb.velocity = new Vector2(moveDelta * speed, rb.velocity.y);
            else
                rb.velocity = new Vector2(moveDelta * airspeed, rb.velocity.y);

        }
    }
    void Jump()
    {
        if (jumpInput)
        {
            if (isGround)
            {
                //myAnim.SetBool("Jump", true);
                if (AudioManager.Instance.PlayerSource.isPlaying)
                {
                    AudioManager.Instance.PlayerSource.Stop();
                }
                animator.SetBool("CanJump", true);
                // 在動畫播放結束後執行回調函數
                float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("StopAnimation", animationLength);

                AudioManager.Instance.PlayPlayer("Jump");

                Vector2 JumpVel = new (0.0f, jumpforce);
                rb.velocity = Vector2.up * JumpVel;
                if (doubleJumpSkill)
                    canDoubleJump = true;
                else
                    canDoubleJump = false;
            }
            else
            {
                if (canDoubleJump)
                {
                    if (AudioManager.Instance.PlayerSource.isPlaying)
                    {
                        AudioManager.Instance.PlayerSource.Stop();
                    }

                    animator.SetBool("CanJump", true);
                    float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
                    Invoke("StopAnimation", animationLength);
                    AudioManager.Instance.PlayPlayer("Jump");
                    
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);
                    rb.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
        if (jumpInputRelease && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
    }

    void StopAnimation()
    {
        animator.SetBool("CanJump", false);
    }

    private void Sprint()
    {
        isInputEnabled = false;
        isSprintable = false;
        isSprintReset = false;

        AudioManager.Instance.PlayPlayer("Dash");

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (transform.localScale.x > 0)
            rb.velocity = new Vector2(sprintSpeed, 0);
        else if (transform.localScale.x < 0)
            rb.velocity = new Vector2(-sprintSpeed, 0);
        StartCoroutine(SprintCoroutine(sprintTime, originalGravity));
    }
    private IEnumerator SprintCoroutine(float sprintDelay, float originalGravity)
    {
        yield return new WaitForSeconds(sprintDelay);
        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(0, 0);
        isInputEnabled = true;
        isSprintable = true;
    }
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Boss")|| coll.gameObject.CompareTag("Enemy"))
        {
 
        }
    }
    private void OnCollisionStay2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = coll.gameObject;
        }
        if (coll.gameObject.CompareTag("Boss")|| coll.gameObject.CompareTag("Enemy"))
        {
        }
    }
    private void OnCollisionExit2D(Collision2D coll)
    {

        if (coll.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
        if (coll.gameObject.CompareTag("Boss")|| coll.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = new Vector2(0,0);
        }
    }
    
       private void OnTriggerEnter2D(Collider2D coll)
       {
            if (coll.gameObject.CompareTag("Trigger")|| coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("Boss"))
            {
                StartCoroutine(KnockBack(coll));
            }
       }

    private void OnTriggerStay2D(Collider2D coll)
    {
        //if (coll.gameObject.CompareTag("Trigger")|| coll.gameObject.CompareTag("Enemy")|| coll.gameObject.CompareTag("Boss"))
        if (coll.gameObject.CompareTag("Boss"))
        {
            if (invincible == false)
            {
                invincibleTimer = invincibleTime;
            }
            Physics2D.IgnoreLayerCollision(7, 6);
            Physics2D.IgnoreLayerCollision(7, 8);
            invincible = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Trigger") || coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("Boss")) 
        {
           invincible = false;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformcollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(capsuleCollider2D, platformcollider);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(capsuleCollider2D, platformcollider, false);
    }
   
   private IEnumerator KnockBack(Collider2D coll)
   {

        canMove = false;
        canJump = false;
        #region  被擊退的方位

            //獲取玩家速度
            float Xdirection;
            float Ydirection;
            //有時候玩家速度會被判定得非常小(EX:4*10^^-6)
            if(Mathf.Abs(rb.velocity.x) > 0.000001) Xdirection = Mathf.Sign(rb.velocity.x) * repel;
            else Xdirection = 0;
            if(Mathf.Abs(rb.velocity.y) > 0.000001) Ydirection = Mathf.Sign(rb.velocity.y) * repel;
            else Ydirection = 0;

            //獲取碰撞到的敵人速度並轉換為被擊飛的速度
            Rigidbody2D enemyRb = coll.GetComponent<Rigidbody2D>();
            float enemyX;
            float enemyY;
            if(enemyRb != null)
            {
                if(Mathf.Abs(enemyRb.velocity.x) > 0.1) enemyX = Mathf.Sign(enemyRb.velocity.x) * repel;
                else enemyX = 0;
                if(Mathf.Abs(enemyRb.velocity.y) > 0.1) enemyY = Mathf.Sign(enemyRb.velocity.y) * repel;
                else enemyY = 0;
            }
            else 
            {
                enemyX = 0;
                enemyY = 0; 
            }

            //1.玩家碰到靜止的敵人
            if(enemyX == 0 && enemyY == 0)  rb.velocity = new Vector2(-1 * Xdirection * repel,-1 * Ydirection * repel);
            
            //2.敵人碰到靜止的玩家
            else if (Xdirection == 0 && Ydirection == 0) rb.velocity = new Vector2(enemyX,enemyY);
              
            //3.敵人和玩家皆不靜止
            else rb.velocity = new Vector2(Xdirection + enemyX/2,Ydirection - enemyY/2);
     
        #endregion
        yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(0,0);
        canMove = true;
        canJump = true;

   }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
