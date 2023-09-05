using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundSetting : MonoBehaviour
{
    CameraMove cameraMove;
    public Vector2 minPos,maxPos;
    void Start()
    {
        cameraMove = GameObject.Find("CameraMove").GetComponent<CameraMove>();
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        cameraMove.SetCamPosLimit(this.minPos,this.maxPos);
    }

}
