using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp = 5;
    public bool resetSavePoint;
    void Start()
    {
        PlayerPrefs.SetInt("Hp", hp);
        PlayerPrefs.SetInt("resetSavePoint", resetSavePoint ? 1 : 0);
    }
    public void button()
    {
        SceneManager.LoadScene("Factory02");
    }
}
