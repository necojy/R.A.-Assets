using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    PlayerMove playerMove;
    PlayerAttack playerAttack;
    Transform parentTransform;
    private void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        parentTransform = transform;

        Hide();
    }
    public void Show()
    {
        Time.timeScale = 0;
        playerMove.isInputEnabled = false;
        playerAttack.isAttackable = false;
        // 遍历所有子物件
        foreach (Transform child in parentTransform)
        {
            // 将每个子物件的SetActive属性设置为false
            child.gameObject.SetActive(true);
        }
    }
    public void Hide()
    {
        Time.timeScale = 1;
        playerMove.isInputEnabled = true;
        playerAttack.isAttackable = true;
        foreach (Transform child in parentTransform)
        {
            // 将每个子物件的SetActive属性设置为false
            child.gameObject.SetActive(false);
        }
    }
    public void home()
    {
        Hide();
        SceneManager.LoadScene("Menu");
    }
    public void exit()
    {
        Application.Quit();
    }
}
