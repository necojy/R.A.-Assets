using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int dmg = 1;
    public float repel = 20f;//被擊退的參數
    private bool ishurt = false;
    private bool detected = true;
    private SpriteRenderer parentSpriteRenderer;
    private Rigidbody2D parentRb;

    private void Start()
    {
        parentSpriteRenderer = transform.parent.parent.GetComponent<SpriteRenderer>();
        parentRb = transform.parent.parent.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && detected)
        {
            StartCoroutine(invincibleTime());
            //Debug.Log("Player Detected! KILL!!!");
            PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - dmg);
        }

        if (other.gameObject.CompareTag("AttackArea"))
        {
            if (!ishurt)
            {
                StartCoroutine(KnockBack(other));
                StartCoroutine(HurtingTime());
            }

        }

    }

    IEnumerator HurtingTime()
    {
        ishurt = true;
        parentSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        parentSpriteRenderer.color = Color.white;
        ishurt = false;
    }

    IEnumerator invincibleTime()
    {
        detected = false;
        yield return new WaitForSeconds(0.5f);
        detected = true;
    }

    private IEnumerator KnockBack(Collider2D coll)
    {
        #region  被擊退的方位

        //獲取玩家速度
        float Xdirection;
        float Ydirection;
        //有時候玩家速度會被判定得非常小(EX:4*10^^-6)
        if (Mathf.Abs(parentRb.velocity.x) > 0.000001) Xdirection = Mathf.Sign(parentRb.velocity.x) * repel;
        else Xdirection = 0;
        if (Mathf.Abs(parentRb.velocity.y) > 0.000001) Ydirection = Mathf.Sign(parentRb.velocity.y) * repel;
        else Ydirection = 0;

        //獲取碰撞到的敵人速度並轉換為被擊飛的速度
        Rigidbody2D enemyRb = coll.GetComponent<Rigidbody2D>();
        float enemyX;
        float enemyY;
        if (enemyRb != null)
        {
            if (Mathf.Abs(enemyRb.velocity.x) > 0.1) enemyX = Mathf.Sign(enemyRb.velocity.x) * repel;
            else enemyX = 0;
            if (Mathf.Abs(enemyRb.velocity.y) > 0.1) enemyY = Mathf.Sign(enemyRb.velocity.y) * repel;
            else enemyY = 0;
        }
        else
        {
            enemyX = 0;
            enemyY = 0;
        }

        //1.玩家碰到靜止的敵人
        if (enemyX == 0 && enemyY == 0) parentRb.velocity = new Vector2(-1 * Xdirection * repel, -1 * Ydirection * repel);

        //2.敵人碰到靜止的玩家
        else if (Xdirection == 0 && Ydirection == 0) parentRb.velocity = new Vector2(enemyX, enemyY);

        //3.敵人和玩家皆不靜止
        else parentRb.velocity = new Vector2(Xdirection + enemyX / 2, Ydirection - enemyY / 2);

        #endregion
        yield return new WaitForSeconds(0.1f);
        parentRb.velocity = new Vector2(0, 0);


    }

    public void PlayerEnterChildCollider()
    {
        StartCoroutine(invincibleTime());
        //Debug.Log("Player Detected! KILL!!!");
        PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - dmg);
    }
}
