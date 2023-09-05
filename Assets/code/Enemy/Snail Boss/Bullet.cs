using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    
    public int disappearTime = 5;
    //private Animation anim;
    bool detected = true;
    public int dmg;
    void Start()
    {
        Destroy(gameObject,disappearTime);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if( other.CompareTag("Player") && detected)
        {
            AudioManager.Instance.PlaySnailBoss("RpgHit");
            StartCoroutine(invincibleTime());
            PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - dmg);
            Destroy(gameObject);
        }
    }
    IEnumerator invincibleTime()
    {
        detected = false;
        yield return new WaitForSeconds(0.5f);
        detected = true;
    }

    
}
