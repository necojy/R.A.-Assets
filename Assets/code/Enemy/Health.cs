using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 3;
    private int max_health = 3;
    private CameraBoundSetting cameraBoundSetting; // 添加 CameraBoundSetting 的引用
    public int dmg;
    private bool detected = true;
    void Start()
    {
        // 在 Start 方法中查找 CameraBoundSetting 並設置引用
        cameraBoundSetting = FindObjectOfType<CameraBoundSetting>();
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

        // 在摧毀之前增加 CameraBoundSetting 的 count
        if (cameraBoundSetting != null)
        {
            cameraBoundSetting.count += 1;
        }

        Destroy(gameObject);
        if (transform.parent.gameObject != null) Destroy(transform.parent.gameObject);
        if (transform.parent.parent.gameObject != null) Destroy(transform.parent.parent.gameObject);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player" && detected)
        {
            StartCoroutine(invincibleTime());
            //Debug.Log("Player Detected! KILL!!!");
            PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - dmg);
        }
        
    }
    IEnumerator invincibleTime()
    {
        detected = false;
        yield return new WaitForSeconds(0.5f);
        detected = true;
    }

}
