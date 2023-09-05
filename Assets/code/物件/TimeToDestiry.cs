using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDestiry : MonoBehaviour
{
    public float timeToDelete = 1f;
    private float countTime = 0f;
    private void Update() {
        countTime += Time.deltaTime;
        if(countTime >= timeToDelete)
        {
            Destroy(gameObject);
        }
    }
}
