using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasd : MonoBehaviour
{
    public GameObject box;
    public GameObject UI_message;

    // Start is called before the first frame update
    void Start()
    {
        UI_message.SetActive(false);  
    }

    void OnTriggerEnter2D (Collider2D player)
    {
        if(player.gameObject.tag == "Player") UI_message.SetActive(true);
    }

    void OnTriggerExit2D (Collider2D player)
    {
        if(player.gameObject.tag == "Player") UI_message.SetActive(false);
    }
}
