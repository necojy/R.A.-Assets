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

    [SerializeField] private int health = 3;
    private int max_health = 3;

    void Start()
    {
        Destroy(gameObject, disappearTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && detected)
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

    public void Damage(int amount)
    {
        this.health -= amount;
        if (this.health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (this.health + amount > max_health)
            this.health = max_health;
        else
            this.health += amount;
    }
    private void Die()
    {
        Debug.Log("Dead");

        Destroy(gameObject);
    }
}
