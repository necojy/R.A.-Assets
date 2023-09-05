using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_break : MonoBehaviour
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
            AudioManager.Instance.PlayItem("BreakFloor");
            Invoke("Delete", WaitTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            CanBreak = true;
        }    
    }
    void Delete()
    {
        Destroy(gameObject);
    }
}
