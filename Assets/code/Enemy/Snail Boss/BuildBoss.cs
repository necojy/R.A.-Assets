using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBoss : MonoBehaviour
{
    public GameObject Bossprefab;
    public Transform InitialPos;
    public float createTime = 1f;
    private CameraMove cameraMove;
    public Transform target;

    #region 螢幕搖晃參數
    public CameraShake cameraShake;
    public float duration = 1.0f;
    public float magnitude = 1f;
    #endregion

    private bool creating = false;
    private PlayerMove playerMove;
    private void Start() 
    {
        cameraMove = GameObject.Find("CameraMove").GetComponent<CameraMove>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        InitialPos.position = new Vector3(InitialPos.position.x -5f,InitialPos.position.y + 5f, InitialPos.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
           if(!creating)
           {
                creating = true;
                
                StartCoroutine(createBoss());
           } 
        }
    }

    IEnumerator createBoss()
    {
        //暫停人物移動
        float tempPlaySpeed = playerMove.speed;
        float tempJumpForce = playerMove.jumpforce;
        playerMove.speed = 0f;
        playerMove.jumpforce = 0;
        playerMove.isInputEnabled = false;

        StartCoroutine(cameraShake.Shake(duration,magnitude));

        //更改相機位置
        if (target != null)
        {
            cameraMove.smoothing = 0.03f;
            cameraMove.target = target;
        }
        Camera.main.fieldOfView = Camera.main.fieldOfView + 8f;
        
        yield return new WaitForSeconds(createTime);
        StartCoroutine(cameraShake.Shake(duration,magnitude));
        Camera.main.fieldOfView = Camera.main.fieldOfView + 8f;
        GameObject Boss = Instantiate(Bossprefab,InitialPos.position,Quaternion.identity);
        yield return new WaitForSeconds(0.5f);

        //恢復人物移動
        playerMove.speed = tempPlaySpeed;
        playerMove.jumpforce = tempJumpForce;
        playerMove.isInputEnabled = true;

        Destroy(gameObject);
    }

}
