using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deadZone : MonoBehaviour
{
    void Reset()
    {
        SceneManager.LoadScene("Menu");
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            AudioManager.Instance.SnailBossBackgroundSource.Stop();
            Reset();
        }
    }

}
