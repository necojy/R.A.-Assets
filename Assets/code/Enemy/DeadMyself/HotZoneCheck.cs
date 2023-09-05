using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private enemyBehavior enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake() 
    {
        enemyParent = GetComponentInParent<enemyBehavior>();
        anim = GetComponentInParent<Animator>();    
    }

    private void Update()
    {
        if(inRange)
        {
            enemyParent.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D  Collider) 
    {
       if(Collider.gameObject.CompareTag("Player"))
       {
            inRange = true;
            enemyParent.target = Collider.transform;
       }
    }

    private void OnTriggerStay2D(Collider2D Collider) 
    {
       if(Collider.gameObject.CompareTag("Player"))
       {
   
       } 
    }
    private void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
            enemyParent.attackMode = false;
        }    
    }
}
