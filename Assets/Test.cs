using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int dmg = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("aaaaa");
        if (other.gameObject.tag == "Player")
        {
            
            PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - dmg);
        }
    }
}
