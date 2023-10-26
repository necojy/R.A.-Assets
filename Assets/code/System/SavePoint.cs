using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SavePoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            string sencesName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetFloat(sencesName + "x", transform.position.x);
            PlayerPrefs.SetFloat(sencesName + "y", transform.position.y);
            Debug.Log("savepoint" + transform.position.x + transform.position.y);
        }
    }
}