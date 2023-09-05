using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class BossHealth : MonoBehaviour
{
    //public Slider healthBar;
    public int health;    
    bool detected = true;
    public int dmg;
    public Transform target;
    private CameraMove cameraMove;

    private void Start() 
    {
        target = GameObject.Find("Player").transform;
        cameraMove = GameObject.Find("CameraMove").GetComponent<CameraMove>();
    }
    public void Update()
    {
        //healthBar.value = health;

        if(health <= 0)
        {
            AudioManager.Instance.SnailBossBackgroundSource.Stop();
            AudioManager.Instance.SnailBossSource.Stop();
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<SpriteRenderer>());

            if (target != null)
            {
                cameraMove.smoothing = 0.03f;
                cameraMove.target = target;
            }
            StartCoroutine(ZoomCameraOut());
            if(Camera.main.fieldOfView <= 56f) 
            {
                cameraMove.smoothing = 1f;
                Destroy(gameObject);
            }
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if( other.CompareTag("Player") && detected)
        {
            StartCoroutine(Invisible());
            PlayerPrefs.SetInt("Hp", PlayerPrefs.GetInt("Hp") - dmg);
        }
    }
    IEnumerator Invisible()
    {
        detected = false;
        yield return new WaitForSeconds(0.5f);
        detected = true;
    }

    private IEnumerator ZoomCameraOut()
    {
        while(Camera.main.fieldOfView > 55f)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,55f,Time.deltaTime * 0.002f);
            yield return null;
        }
    }
}
