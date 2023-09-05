using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossRoom : MonoBehaviour
{
    #region 要消失的物件
    public CollapseFloor[] collapseFloor;  
    private int count = 0;
    #endregion
    
    #region 螢幕搖晃參數
    public CameraShake cameraShake;
    public float duration = 1.0f;
    public float magnitude = 1f;
    #endregion
    
    private CameraMove cameraMove;

    private void Start() 
    {
        cameraMove = GameObject.Find("CameraMove").GetComponent<CameraMove>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        { 
            StartCoroutine(TimeToBrakFloor());
            StartCoroutine(cameraShake.Shake(duration,magnitude));
            StartCoroutine(TimeToDel());
        }
    } 
    IEnumerator TimeToBrakFloor()
    {
        while(count < collapseFloor.Length)
        {
            if(collapseFloor[count] != null) collapseFloor[count++].CanBreak = true;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator TimeToDel()
    {
        Camera.main.fieldOfView = 65f;
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
