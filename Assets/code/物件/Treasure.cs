using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    bool SetHp = true;
    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Debug.Log("Hello, this is a debug message!");
            if (SetHp)
            {
                PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") + 1);
                SetHp = false;
            }
        }
    }
}
