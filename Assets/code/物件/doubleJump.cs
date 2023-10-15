using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJump : MonoBehaviour
{
    #region reference
    public GameObject floor;
    public GameObject floorPosition;
    public Vector2 startPosition;
    public bool doubleJumpSkill = false;
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
            doubleJumpSkill = true;
            floor.transform.position = startPosition;
            floor.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

    }


}
