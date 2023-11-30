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

        if (AudioManager.Instance == null)
        {
            // 初始化 AudioManager 或者在這裡進行其他必要的處理
            Debug.LogError("AudioManager is not initialized!");
            return;
        }

        if (currentScene.name == "Factory02")
        {
            AudioManager.Instance.PlayBackground("Factory Map Music");
        }
    }
    public void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (AudioManager.Instance == null)
        {
            // 初始化 AudioManager 或者在這裡進行其他必要的處理
            Debug.LogError("AudioManager is not initialized!");
            return;
        }

        if (currentScene.name == "Factory02")
        {
            AudioManager.Instance.PlayBackground("Factory Map Music");
        }
    }
}
