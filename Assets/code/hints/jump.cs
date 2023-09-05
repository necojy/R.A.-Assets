using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public GameObject box;
    public GameObject UImessage;

    // Start is called before the first frame update
    void Start()
    {
        UImessage.SetActive(false);  
    }

    void OnTriggerEnter2D (Collider2D player)
    {
        if(player.gameObject.tag == "Player") UImessage.SetActive(true);
    }

    void OnTriggerExit2D (Collider2D player)
    {
        if(player.gameObject.tag == "Player") UImessage.SetActive(false);
    }
}
