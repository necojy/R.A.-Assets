using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehaviour : MonoBehaviour
{
    public int dmg;
    bool detected = true;//can change based on player skill, if player could dash, this should be false temporarily. Else, this stays true all the time.

    IEnumerator invincibleTime()
    {
        detected = false;
        yield return new WaitForSeconds(0.5f);
        detected = true;
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player" && detected)
        {
            StartCoroutine(invincibleTime());
            //Debug.Log("Player Detected! KILL!!!");
            PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - dmg);
        }
    }
}
