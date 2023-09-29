using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportBelt : MonoBehaviour
{
   public float speed = 5f;
   public bool moveToRight = false;
   private float playerTempMoveSpeed;
   private void Start() 
   {
        GameObject Player = GameObject.Find("Player");
        if(Player != null)
        {
            PlayerMove move = Player.GetComponent<PlayerMove>();
            playerTempMoveSpeed = move.speed;
        }
   }


   private void OnCollisionStay2D(Collision2D other)
   {
        Rigidbody2D rb = other.collider.GetComponent<Rigidbody2D>();
        float signX = Mathf.Sign(rb.velocity.x);

        if (rb != null)
        {
            if(other.gameObject.tag == "Player")
            {
                PlayerMove move = other.collider.GetComponent<PlayerMove>();
                if(moveToRight)
                {             
                    if(signX > 0) move.speed = playerTempMoveSpeed + speed;
                    else if(signX < 0) move.speed = playerTempMoveSpeed - speed;
                }

                else
                {
                    if(signX > 0) move.speed = playerTempMoveSpeed - speed;
                    else if(signX < 0) move.speed = playerTempMoveSpeed + speed;
                }
            }
            // 將物體的速度設定為傳送帶速度的向右向量
            if(moveToRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }

            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            
        
        }
   }


    private void OnCollisionExit2D(Collision2D other) 
    {
        Rigidbody2D rb = other.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if(other.gameObject.tag == "Player")
            {
                PlayerMove move = other.collider.GetComponent<PlayerMove>();
                move.speed = playerTempMoveSpeed;
            }
        }
    }
}
