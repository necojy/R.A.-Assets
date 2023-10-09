using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    #region public 
    public float attackDistance = 5f;
    public float moveSpeed = 5f,rushSpeed = 10f;  
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //判斷玩家是否進入攻擊範圍
    [HideInInspector] public bool attackMode = false;
    public GameObject hotZone;
    public GameObject triggerArea;
 
    #endregion


    void Awake()
    {
        SelectTarget(); //選擇target
    }
    void Update()
    {
        //非攻擊模式，移動到target(左右點)
        if(!attackMode) Move();

        //攻擊模式，移動到玩家位置
        else Attack();
        
        //如果超出巡邏範圍且玩家離開攻擊範圍則回到巡邏範圍
        if(!InsideofLimits() && !inRange) SelectTarget();
        
        //如果玩家在攻擊範圍內，此時target為Player(TriggerArea.cs)
        if(inRange)  EnemyLogic();
        
        
    }

    void EnemyLogic()//如果在範圍內(HotZone)則變成攻擊模式
    {       
        if(!inRange) attackMode = false;  
        else if(inRange) attackMode   = true;    
    }
    void Move()
    {
        Vector2 targetPosition = new Vector2(target.position.x,target.position.y + 2f);
        transform.position = Vector2.MoveTowards(transform.position,targetPosition,moveSpeed * Time.deltaTime);  
    }

    void Attack()
    {
        
        AudioManager.Instance.PlayMonster("DeadMyself");
        
     
        transform.position = Vector2.MoveTowards(transform.position,target.position,rushSpeed * Time.deltaTime);
    }
    private bool InsideofLimits()//判斷是否在左端點及右端點內
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()//判斷往左端點走還是又右端點走，判斷完將目的地設為target 
    {
        float distanceTOLeft = Vector2.Distance(transform.position,leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position,rightLimit.position);

        if(distanceTOLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    
    
}
