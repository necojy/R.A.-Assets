using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player" && PlayerPrefs.GetInt("savePoint") != transform.GetSiblingIndex())
        {
            AudioManager.Instance.PlayUI("SavePoint");
            PlayerPrefs.SetInt("savePoint", transform.GetSiblingIndex());
            //PlayerPrefs.SetInt("saveHp", PlayerPrefs.GetInt("Hp"));
            //PlayerPrefs.SetInt("saveState", PlayerPrefs.GetInt("State"));
            Debug.Log("Savingpoint" + transform.GetSiblingIndex());
        }
    }
}