using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJump : MonoBehaviour
{
    #region reference
    public GameObject floor;
    public GameObject floorPosition;
    public Vector2 startPosition;
    #endregion

    void Start()
    {
        floor.gameObject.SetActive(true);
        startPosition = floor.transform.position;
        floor.transform.position = floorPosition.transform.position;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("doubleJumpSkill", 1);
            floor.transform.position = startPosition;
            floor.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

    }


}
