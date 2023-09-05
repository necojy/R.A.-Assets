using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject coin;

    public float coinSpeed = 5f;//向外拋出金幣的速度
    private bool canOpen;//確認玩家是否能開寶箱
    private bool BoxStatus;//確認寶箱是否被開啟
    private Animator anim;


    void Start()
    {
        BoxStatus = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(canOpen && !BoxStatus)
            {
                BoxStatus = true;
                SpawnCoins();
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D Other)
    {
        if(Other.gameObject.CompareTag("Player"))
        {
            canOpen = false;
        }
    }

    void SpawnCoins()
    {
        AudioManager.Instance.PlayItem("Treature");
        Rigidbody2D coinInstance = Instantiate(coin, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        // 設置金幣的旋轉角度
        Rigidbody2D coinRigidbody = coinInstance.GetComponent<Rigidbody2D>();
        coinRigidbody.AddForce(Vector2.up * coinSpeed, ForceMode2D.Impulse);
    }
}
