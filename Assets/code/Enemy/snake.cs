using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    public float moveSpeed;//不能超過0.5
    public BoxCollider2D feet;
    public BoxCollider2D front;
    public bool isGround;
    public bool touch_wall;
    public float a, b;
    bool first = false;
    private void Start()
    {
        moveSpeed = moveSpeed > 0.5f ? 0.5f : moveSpeed;
    }
    private void FixedUpdate()
    {
        isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        touch_wall = front.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isGround && !touch_wall)
        {
            first = true;
            transform.position += dir(moveSpeed, 0);
        }
        else
        {
            if (first)
            {
                flip();
                first = false;
            }
        }
    }
    void flip()
    {
        if (!isGround)
        {
            add_angle(-1);
            transform.position += dir(a, -moveSpeed * 2);
        }
        else
        {
            transform.position += dir(0, a);
            add_angle(1);
        }
    }
    Vector3 dir(float x, float y)
    {
        switch (transform.eulerAngles.z)
        {
            case 0:
                return new Vector3(x, y, 0);
            case 90:
                return new Vector3(-y, x, 0);
            case 180:
                return new Vector3(-x, -y, 0);
            case 270:
                return new Vector3(y, -x, 0);
        }
        return Vector3.zero;
    }
    void add_angle(int angle)
    {
        angle = (int)transform.eulerAngles.z / 90 + angle;
        if (angle > 3) angle = 0;
        else if (angle < 0) angle = 3;
        transform.eulerAngles = new Vector3(0, 0, angle * 90);
    }
}
