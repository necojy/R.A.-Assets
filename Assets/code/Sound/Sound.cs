using UnityEngine.Audio;
using UnityEngine;


//在unity裡，自定義資料型別無法顯示在inspectior面板裡
//需要對定義資料型別的類或者結構體使用[System.Serializable]
//參考https://www.796t.com/content/1548321125.html
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
