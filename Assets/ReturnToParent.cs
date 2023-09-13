using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToParent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            transform.parent.GetComponent<HitBox>().PlayerEnterChildCollider();
        }
    }
}
