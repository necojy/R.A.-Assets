using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_system : MonoBehaviour
{
    public int Maxhp;
    Vector3 Player_pos;
    GameObject player;
    public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        if (reset) PlayerPrefs.SetInt("savePoint", 0);
        PlayerPrefs.SetInt("Hp", Maxhp);
        player = GameObject.FindWithTag("Player");

        //PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("saveHp"));
        player.transform.position = gameObject.transform.GetChild(PlayerPrefs.GetInt("savePoint")).gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
