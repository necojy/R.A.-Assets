using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoney : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GenerateMoney());         
        }
    }
    
    IEnumerator GenerateMoney()
    {
        yield  return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    
}
