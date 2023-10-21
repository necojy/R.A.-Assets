using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Hp") == 0)
            PlayerPrefs.SetInt("Hp", 5);
    }
    public void button()
    {
        SceneManager.LoadScene("Factory02");
    }
}
