using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
   private enemyBehavior enemyParent;

   private void Awake()
   {
     enemyParent = GetComponentInParent<enemyBehavior>();
     enemyParent.hotZone.SetActive(false);

     //設定BoxCollider2D尺寸
     BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
     boxCollider.size = new Vector3(3f, Mathf.Abs(enemyParent.attackDistance * 2), 0f);
     boxCollider.offset = new Vector3(0f, -1 * enemyParent.attackDistance - 1f, 0f);
   }

   private void OnTriggerEnter2D(Collider2D collider) 
   {
        if(collider.gameObject.CompareTag("Player")) 
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
   }
}
