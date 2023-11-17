using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attackarea : MonoBehaviour
{
    private int damage = 1;
    private Vector3 pos;
    public Vector3 targetPos;
    private void Start()
    {
        pos = GameObject.Find("Player").transform.position;
    }
    private void Update()
    {
        pos = GameObject.Find("Player").transform.position;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            if (coll.GetComponent<Health>() != null)
            {
                Health health = coll.GetComponent<Health>();
                health.Damage(damage);
                Debug.Log("hit");

                //會穿牆
                // targetPos = coll.transform.position;
                // if ((targetPos.x - pos.x) > 0) 
                // {
                //     Debug.Log("R");
                //     coll.transform.position = new Vector3((coll.transform.position.x + 1), coll.transform.position.y,0);
                // }
                // else if ((targetPos.x - pos.x) < 0)
                // {
                //     Debug.Log("L");
                //     coll.transform.position = new Vector3((coll.transform.position.x - 1), coll.transform.position.y,0);
                // }
            }
            if (coll.GetComponent<Bullet>() != null)
            {
                Bullet health = coll.GetComponent<Bullet>();
                health.Damage(damage);
                Debug.Log("hit");
            }
        }

        else if (coll.gameObject.CompareTag("Boss"))
        {
            if(coll.GetComponent<BossHealth>() != null)
            {
                BossHealth bossHealth = coll.GetComponent<BossHealth>();
                bossHealth.Damage(damage);
            }
        }
    }
}
