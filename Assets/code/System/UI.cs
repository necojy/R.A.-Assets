using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public ObjectStack heart;
    public float Heart_space;   
    Transform Player_pos;
    public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        Player_pos = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        SetHp();
    }
    void SetHp()
    {
        if (PlayerPrefs.GetInt("Hp") > 0 && Player_pos.position.y > -50)
        {
            int change = PlayerPrefs.GetInt("Hp") - heart.get_top();
            if (change != 0) Debug.Log(change);
            if (change > 0)
            {
                for (int i = 0; i < change; i++)
                {
                    heart.add(Heart_space);
                }
            }
            else if (change < 0)
            {
                for (int i = 0; i < -change; i++)
                    heart.remove();
            }
        }
        else
        {
            Reset();
        }
    }

    void Reset()
    {
        SceneManager.LoadScene("Menu");
    }
}
