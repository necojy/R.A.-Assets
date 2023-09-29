using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用Array需呼叫
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Sound[] PlayerSound, MonsterSound, SnailBoosSound, ItemSound, UISound, BackgroundSound, SnailBackgroundSound;
    public AudioSource PlayerSource, MonsterSource, SnailBossSource, ItemSource, UISource, BackgroundSource, SnailBossBackgroundSource;
    [HideInInspector] public Boolean isWalkPlaying = false;
    [HideInInspector] public Boolean isDrogenPlaying = false;
    [HideInInspector] public Boolean isDeadMyselfPlaying = false;
    [HideInInspector] public Boolean isRobotWalkPlaying = false;
    [HideInInspector] public Boolean isFiringRpgAPlaying = false;
    [HideInInspector] public Boolean isFiringRpgBPlaying = false;
    [HideInInspector] public Boolean isTransportBeltPlaying = false;


    public static Sound currentPlayingSound;
    private Coroutine playWalkCoroutine;

    public void PlayPlayer(string name)
    {
        Sound s = Array.Find(PlayerSound, x => x.name == name);
        // if( s != null) currentPlayingSound = s;
        if (s == null)
        {
            Debug.Log(name + "Sound not found");
        }

        else if (s.name == "Walk")
        {
            if (isWalkPlaying)
            {
                return;
            }
            else
            {
                StartCoroutine(PlayPlayerWalkCoroutine(s));
            }
        }
        else if (s.name == "Jump")
        {
            currentPlayingSound = s;
            PlayerSource.PlayOneShot(s.clip);
        }
        else if (s.name == "Attack")
        {
            PlayerSource.PlayOneShot(s.clip);
        }
        else if (s.name == "Dash")
        {
            PlayerSource.PlayOneShot(s.clip);
        }
    }
    public void PlayMonster(string name)
    {
        //使用一個參數 x 來遍歷 MonsterSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(MonsterSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else if (s.name == "Drogen")
        {
            if (isDrogenPlaying)
            {
                return;
            }
            else
            {
                StartCoroutine(PlayDrogrenCoroutine(s));
            }
        }
        else if (s.name == "DeadMyself")
        {
            if (isDeadMyselfPlaying)
            {
                return;
            }
            else
            {
                StartCoroutine(PlayDeadMyselfrCoroutine(s));
            }
        }
        else if (s.name == "RobotWalk")
        {
            if (isRobotWalkPlaying)
            {
                return;
            }
            else
            {
                StartCoroutine(PlayRobotWalkCoroutine(s));
            }
        }
    }
    public void PlayItem(string name)
    {
        //使用一個參數 x 來遍歷 ItemSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(ItemSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else if (s.name == "Treature")
        {
            ItemSource.PlayOneShot(s.clip);
        }
        else if (s.name == "BreakFloor")
        {
            ItemSource.PlayOneShot(s.clip);
        }
        else if (s.name == "RobotcArm00")
        {
            ItemSource.PlayOneShot(s.clip);
        }
        else if (s.name == "RobotcArm01")
        {
            ItemSource.PlayOneShot(s.clip);
        }
        else if (s.name == "TransportBelt")
        {
            if (isTransportBeltPlaying)
            {
                return;
            }
            else
            {
                StartCoroutine(PlayTransportBeltCoroutine(s));
            }
        }
    }

    public void PlayUI(string name)
    {
        //使用一個參數 x 來遍歷 ItemSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(UISound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else if (s.name == "SavePoint")
        {
            UISource.PlayOneShot(s.clip);
        }

    }

    private IEnumerator PlayPlayerWalkCoroutine(Sound s)
    {
        isWalkPlaying = true;
        PlayerSource.PlayOneShot(s.clip);
        yield return new WaitForSeconds(s.clip.length);
        isWalkPlaying = false;
    }
    private IEnumerator PlayDrogrenCoroutine(Sound s)
    {
        isDrogenPlaying = true;
        MonsterSource.PlayOneShot(s.clip);
        yield return new WaitForSeconds(s.clip.length);
        isDrogenPlaying = false;
    }
    private IEnumerator PlayDeadMyselfrCoroutine(Sound s)
    {
        isDeadMyselfPlaying = true;
        MonsterSource.PlayOneShot(s.clip);
        yield return new WaitForSeconds(s.clip.length);
        isDeadMyselfPlaying = false;
    }

    private IEnumerator PlayRobotWalkCoroutine(Sound s)
    {
        isRobotWalkPlaying = true;
        MonsterSource.PlayOneShot(s.clip);
        yield return new WaitForSeconds(s.clip.length);
        isRobotWalkPlaying = false;
    }
    private IEnumerator PlayTransportBeltCoroutine(Sound s)
    {
        isTransportBeltPlaying = true;
        ItemSource.PlayOneShot(s.clip);
        yield return new WaitForSeconds(s.clip.length);
        isTransportBeltPlaying = false;
    }

    public void PlaySnailBoss(string name)
    {
        //使用一個參數 x 來遍歷 MonsterSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(SnailBoosSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else if (s.name == "FiringRpgA")
        {
            if (isFiringRpgAPlaying)
            {
                return;
            }
            else
            {
                StartCoroutine(PlaySnailBossFiringRpgACoroutine(s));
            }
        }

        else if (s.name == "FiringRpgB")
        {
            if (isFiringRpgBPlaying)
            {
                return;
            }
            else
            {
                StartCoroutine(PlaySnailBossFiringRpgBCoroutine(s));
            }
        }

        else if (s.name == "RpgHit")
        {
            SnailBossSource.PlayOneShot(s.clip);
        }

        else if (s.name == "Rush")
        {
            SnailBossSource.PlayOneShot(s.clip);
        }
        else if (s.name == "LaserEyes")
        {
            SnailBossSource.PlayOneShot(s.clip);
        }
    }
    private IEnumerator PlaySnailBossFiringRpgACoroutine(Sound s)
    {
        isFiringRpgAPlaying = true;
        SnailBossSource.PlayOneShot(s.clip);
        yield return new WaitForSeconds(s.clip.length);
        isFiringRpgAPlaying = false;
    }
    private IEnumerator PlaySnailBossFiringRpgBCoroutine(Sound s)
    {
        isFiringRpgBPlaying = true;
        SnailBossSource.PlayOneShot(s.clip);
        yield return new WaitForSeconds(s.clip.length);
        isFiringRpgBPlaying = false;
    }

    public void PlayBackground(string name)
    {
        //使用一個參數 x 來遍歷 ItemSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(BackgroundSound, x => x.name == name);

        BackgroundSource.Stop();
        BackgroundSource.loop = true;

        if (s == null)
        {
            Debug.Log(name + "Sound not found");
        }
        else if (s.name == "Factory Map Music")
        {
            SnailBossBackgroundSource.clip = s.clip;
            SnailBossBackgroundSource.Play();
        }
    }
    public void PlaySnailBossBackground(string name)
    {
        //使用一個參數 x 來遍歷 ItemSound 陣列中的元素，並檢查每個元素的 name 屬性是否等於 name 參數。
        Sound s = Array.Find(SnailBackgroundSound, x => x.name == name);

        SnailBossBackgroundSource.Stop();
        SnailBossBackgroundSource.loop = true;

        if (s == null)
        {
            Debug.Log(name + "Sound not found");
        }
        else if (s.name == "SnailBossFirstType")
        {
            SnailBossBackgroundSource.clip = s.clip;
            SnailBossBackgroundSource.Play();
        }
        else if (s.name == "SnailBossSecondType")
        {
            SnailBossBackgroundSource.clip = s.clip;
            SnailBossBackgroundSource.Play();
        }

    }
}