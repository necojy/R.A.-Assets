using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Transform max_point, min_point;
    public Vector2 offset;
    public double smoothing = 1;

    public Vector2 minPos, maxPos;

    private void Start()
    {
        minPos.x = min_point.position.x;
        minPos.y = min_point.position.y;
        maxPos.x = max_point.position.x;
        maxPos.y = max_point.position.y;
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = new Vector3(target.position.x - offset.x, target.position.y - offset.y, target.position.z);
                targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x); //用於將一個值(targetPos.x)限制在指定的範圍內(min,max)。
                targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);//+ 0.25f; 
                transform.position = Vector3.Lerp(transform.position, targetPos, (float)smoothing);

            }
        }
    }

    public void SetCamPosLimit(Vector2 minPos, Vector2 maxPos)
    {
        this.minPos = minPos;
        this.maxPos = maxPos;
    }
}
