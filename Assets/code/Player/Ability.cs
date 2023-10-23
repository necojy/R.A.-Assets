using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public Image sprintImage;
    public float sprintInterval= 1.0f;
    public float sprint_cooldownTimer = 0.0f;
    public bool isSprintcooldown;

    PlayerMove playerMove;

    void Start()
    {
        sprintImage.fillAmount = 0;
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void Update()
    {
        Sprint();
    }
    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.K) && isSprintcooldown == false)
        {
            isSprintcooldown = true;
            sprintImage.fillAmount = 1;
        }
        if (isSprintcooldown)
        {
            sprint_cooldownTimer += Time.deltaTime;
            sprintImage.fillAmount = (sprintInterval - sprint_cooldownTimer) / sprintInterval;
            if(sprint_cooldownTimer >= sprintInterval)
            {
                sprintImage.fillAmount = 0;
                sprint_cooldownTimer = 0.0f;
                isSprintcooldown = false;
                playerMove.isSprintReset = true;
            }
        }
    }

}
