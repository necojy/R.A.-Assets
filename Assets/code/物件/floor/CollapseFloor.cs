using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseFloor : MonoBehaviour
{
    
    private Animator anim;
    public bool CanBreak = false;
    public float WaitTime = 1f;
    void Start()
    {
        anim = GetComponent<Animator>();     
    }
    private void Update() 
    {
        if(CanBreak)
        {
            anim.SetBool("CanBreak",true);
            Invoke("Delete", WaitTime);
        }
    }
    void Delete()
    {
        Destroy(gameObject);
    }
}
