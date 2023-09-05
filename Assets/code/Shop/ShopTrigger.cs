using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    Shop shop;
    private bool inshop;
    void Start()
    {
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        shop.Hide();
        Time.timeScale = 1;
        inshop=false;
    }


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I)) && shop.isshow) 
        {
            shop.Hide();
        }
        else if (Input.GetKeyDown(KeyCode.I) && inshop) 
        {
            shop.Show();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inshop=true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inshop =false;
    }
}
