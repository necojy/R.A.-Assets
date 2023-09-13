using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBoundSetting : MonoBehaviour
{
    CameraMove cameraMove;
    GenerateDeadMySelf generateDeadMySelf;
    public Vector2 minPos, maxPos;
    public GameObject toLeftDoor, toUpDoor;
    public GameObject toLeftDoorTarget,toUpDoorTarget;
    public float doorSpeed = 2f;
    private Vector2 tempMinPos, tempMaxPos;
    [HideInInspector] public float count = 0f;


    void Start()
    {
        cameraMove = GameObject.Find("CameraMove").GetComponent<CameraMove>();
        generateDeadMySelf = FindObjectOfType<GenerateDeadMySelf>();
        tempMaxPos = cameraMove.maxPos;
        tempMinPos = cameraMove.minPos;
        if (toLeftDoor != null) toLeftDoor.SetActive(false);
        if (toUpDoor != null) toUpDoor.SetActive(false);
    }
    void Update()
    {
        if (count == generateDeadMySelf.generatePos.Length)
        {
            cameraMove.SetCamPosLimit(tempMinPos, tempMaxPos);
            StartCoroutine(ZoomCameraOut());
            Destroy(toLeftDoor);
            Destroy(toLeftDoorTarget);
            Destroy(toUpDoor);
            Destroy(toUpDoorTarget);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        cameraMove.SetCamPosLimit(this.minPos, this.maxPos);
        if (toLeftDoor != null) 
        {
            toLeftDoor.SetActive(true);
            toLeftDoor.transform.position = Vector2.MoveTowards(toLeftDoor.transform.position,toLeftDoorTarget.transform.position,doorSpeed * Time.deltaTime);
        }
        if (toUpDoor != null) 
        {
            toUpDoor.SetActive(true);
            toUpDoor.transform.position = Vector2.MoveTowards(toUpDoor.transform.position,toUpDoorTarget.transform.position,doorSpeed * Time.deltaTime);
        }
    }

    private IEnumerator ZoomCameraOut()
    {
        while (Camera.main.fieldOfView > 55f)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 55f, Time.deltaTime * 0.002f);
            yield return null;
        }
    }

}
