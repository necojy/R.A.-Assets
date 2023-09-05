using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    PlayerMove playerMove;
    PlayerAttack playerAttack;
    public GameObject play_button;
    private void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        play_button.SetActive(false);
    }
    public void Show()
    {
        Time.timeScale = 0;
        playerMove.isInputEnabled = false;
        playerAttack.isAttackable = false;
        play_button.SetActive(true);
    }
    public void Hide()
    {
        Time.timeScale = 1;
        playerMove.isInputEnabled = true;
        playerAttack.isAttackable = true;
        play_button.SetActive(false);
    }
}
