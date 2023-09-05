using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_cush : MonoBehaviour
{
    
    public int damage = 2;
    private bool canTakeDamage = true;

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.GetComponent<Health>() != null && other.CompareTag("Player") && canTakeDamage)
        {
            
            Health health = other.GetComponent<Health>();
            health.Damage(damage); 
            canTakeDamage = false;
            StartCoroutine(ResetCanTakeDamage());
        }
    }

    
    // private void OnCollisionStay2D(Collision2D other) 
    // {
    //     Collider2D otherCollider = other.collider;
    //     if(otherCollider.GetComponent<Health>() != null && otherCollider.CompareTag("Player") && canTakeDamage)
    //     {
            
    //         Health health = otherCollider.GetComponent<Health>();
    //         health.Damage(damage); 
    //         canTakeDamage = false;
    //         StartCoroutine(ResetCanTakeDamage());
    //     }
    // }
    IEnumerator ResetCanTakeDamage()
    {
        yield return new WaitForSeconds(damage);
        canTakeDamage = true;
    }

}

