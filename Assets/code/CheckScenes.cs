using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class CheckScenes : MonoBehaviour
{
    //檢查當前場景
    public void Start() 
    { 
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Factory02")
        {
            AudioManager.Instance.PlayBackground("Factory Map Music");
        }
    }
}
