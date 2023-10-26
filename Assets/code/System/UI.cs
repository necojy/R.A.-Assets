using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public ObjectStack heart;
    public float Heart_space;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        string sencesName = SceneManager.GetActiveScene().name;
        Player = GameObject.FindWithTag("Player");
        //確定要移位
        if (PlayerPrefs.GetInt("resetSavePoint") == 0)
        {
            float px = PlayerPrefs.GetFloat(sencesName + "x");
            float py = PlayerPrefs.GetFloat(sencesName + "y");
            Player.transform.position = new Vector3(px, py, 0);
        }
        else
        {
            PlayerPrefs.SetFloat(sencesName + "x", Player.transform.position.x);
            PlayerPrefs.SetFloat(sencesName + "y", Player.transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetHp();
    }
    void SetHp()
    {
        if (PlayerPrefs.GetInt("Hp") > 0 && Player.transform.position.y > -100)
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
            AudioManager.Instance.SnailBossBackgroundSource.Stop();
            Reset();
        }
    }

    void Reset()
    {
        SceneManager.LoadScene("Menu");
    }
}
