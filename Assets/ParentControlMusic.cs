using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentControlMusic : MonoBehaviour
{
    public float HearDistance = 10f; //能聽到的距離
    public string musicName;
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < HearDistance)
        {
            AudioManager.Instance.PlayItem(musicName);
        }
    }
}
