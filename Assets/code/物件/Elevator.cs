using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    public float duration = 2f;
    public float magnitude = 0.4f;
    public string sceneName;
    public CameraShake cameraShake;
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.W))
            {
                StartCoroutine(cameraShake.Shake(duration,magnitude));
                StartCoroutine(ChangeScenes());
            }
        }
    }

    IEnumerator ChangeScenes()
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneName);
    }
}
