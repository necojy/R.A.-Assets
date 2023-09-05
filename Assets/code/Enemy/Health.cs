using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]private int health = 3;
    private int max_health = 3;
    

    public void Damage(int amount)
    {
        this.health -= amount;
        if(this.health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (this.health + amount > max_health)
            this.health=max_health;
        else
            this.health += amount;
    }
    private void Die()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
        if(transform.parent.gameObject != null) Destroy(transform.parent.gameObject);
        if(transform.parent.parent.gameObject != null) Destroy(transform.parent.parent.gameObject);
    }
}
