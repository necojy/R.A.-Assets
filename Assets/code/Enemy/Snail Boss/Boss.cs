using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;
public class Boss : MonoBehaviour {  

    #region 子彈參數
        public GameObject bulletPrefab;//生成物件
        public Transform A,B;//生成點
        public float bulletInterval = 1.0f;//子彈間隔
        public float bulletSpeed = 30f;//子彈速度
        #endregion
        
    #region 衝刺參數
        public float rushSpeed = 30.0f; //衝刺速度
        public float RushTime = 1f;//衝刺冷卻
        public bool crush = false;
        #endregion 

    #region Boss基礎參數設定
    private BossHealth BossHealth;
    private float TotalHeal; //Boss血量
    public int damage = 2; //Boss基礎傷害
    private bool MoveToLeft = true; //Boss前進方向
    private bool modeOpen = true;//Boss選擇攻擊模式
    private int mdoeNumber = -1;//Boss攻擊模式號碼
    private int firstTempmdoeNumber = -1;//Boss攻擊模式號碼暫存1
    private int secondTempmdoeNumber = -1;//Boss攻擊模式號碼暫存2
    public float modeIntervalTime = 2.5f;//Boss攻擊模式間隔時間
    private  Animator animator;
    #endregion
    
    #region boss型態切換 
    private bool firstType = false;
    private bool SecondType = false;
    private bool thirdType = false;
    #endregion
     
    #region 螢幕搖晃參數
    private CameraShake cameraShake;
    public float duration = 1.0f;
    public float magnitude = 0.4f;
    #endregion

    void Start () 
    {              
        //獲取Boss動畫
        animator = GetComponent<Animator>();

        //獲取Boss血量 
        BossHealth = GetComponentInChildren<BossHealth>();
        TotalHeal = BossHealth.health;       

        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }
    private void Update() 
    {     
        #region boss型態切換

        if(BossHealth.health > (TotalHeal / 4 * 3))
        {
            if(!firstType)
            {
                AudioManager.Instance.PlaySnailBossBackground("SnailBossFirstType");
                firstType = true;
            }
        }

        else if(BossHealth.health <= (TotalHeal / 4 * 3) && BossHealth.health >= (TotalHeal / 4 * 2))
        {      
            SecondType = true;
        }

        else if(BossHealth.health < (TotalHeal / 4 * 2))
        {
            if(!thirdType)
            {
                AudioManager.Instance.PlaySnailBossBackground("SnailBossSecondType");
                thirdType = true;
            }
        }

        #endregion  
        
        #region boss型態攻擊方式
        if(modeOpen)
        {
            modeOpen = false;
            StartCoroutine(ModeSelect());
        }
        if(mdoeNumber == 1)
        {
            StartCoroutine(ShootCoroutine());   
        }       
        else if(mdoeNumber == 2)
        {
            StartCoroutine(LaserEyesCoroutine());  
        }
        else if(mdoeNumber == 3)
        {  
            StartCoroutine(RushCoroutine()); 
        }
        #endregion  
    }

    //控制射擊模式時間
    IEnumerator ShootCoroutine() 
    {
        mdoeNumber = -1;

        animator.SetBool("initialShoot",true);
        yield return new WaitForSeconds(0.3f); //播放動畫時間
        animator.SetBool("initialShoot",false);
        ShootUnder();

        yield return new WaitForSeconds(bulletInterval);//子彈間格時間

        animator.SetBool("initialShoot",true);
        yield return new WaitForSeconds(0.3f); //播放動畫時間
        animator.SetBool("initialShoot",false);
        Shoot1Above();

        yield return new WaitForSeconds(bulletInterval);//子彈間格時間

        animator.SetBool("initialShoot",true);
        yield return new WaitForSeconds(0.3f); //播放動畫時間
        animator.SetBool("initialShoot",false);
        ShootUnder();
    }

    //控制雷射眼模式時間
    IEnumerator LaserEyesCoroutine()
    {
        mdoeNumber = -1;
        animator.SetBool("toSecond",true);
        yield return new WaitForSeconds(0.5f);

        AudioManager.Instance.PlaySnailBoss("LaserEyes");
        animator.SetBool("secondLaserEyes",true);
        yield return new WaitForSeconds(1f);

        animator.SetBool("secondLaserEyes",false);
        animator.SetBool("toSecond",false);
    }

    //控制衝刺模式時間
    IEnumerator RushCoroutine() 
    {
        mdoeNumber = -1;
        AudioManager.Instance.PlaySnailBoss("Rush");

        //更改火箭方向
        Vector3 scale =  bulletPrefab.transform.localScale;  
        bulletPrefab.transform.localScale = new Vector3(scale.x*-1,scale.y,scale.z);
            
        animator.SetBool("toSecond",true);
        animator.SetBool("toThird",true);
        yield return new WaitForSeconds(0.5f);

        animator.SetBool("thirdAccelerate",true);
        yield return new WaitForSeconds(0.5f);
        

        animator.SetBool("thirdRush",true);
        Vector2 bossVelocity;
        if(MoveToLeft)  bossVelocity = new Vector2(-1f,0f).normalized * rushSpeed;
        else  bossVelocity = new Vector2(1f,0f).normalized * rushSpeed;
        transform.GetComponent<Rigidbody2D>().velocity = bossVelocity;
        yield return new WaitForSeconds(RushTime);
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Flip();

        animator.SetBool("thirdRush",false);
        animator.SetBool("thirdAccelerate",false);
        animator.SetBool("toThird",false);
        animator.SetBool("toSecond",false);

    }
    
    //模式選擇
    IEnumerator ModeSelect()
    {
        //只有基礎模式
        if(firstType && !SecondType && !thirdType) mdoeNumber = 1;
        else if(firstType && SecondType && !thirdType) mdoeNumber = Random.Range(1,3);
        else if(firstType && SecondType && thirdType) 
        {
            mdoeNumber = Random.Range(1,4);

            //確保同一動作不會超過兩次
            if(firstTempmdoeNumber == mdoeNumber && secondTempmdoeNumber == mdoeNumber)
            {
                Debug.Log("repete");
                mdoeNumber = (mdoeNumber + 1) % 3 + 1;
            }

            firstTempmdoeNumber = mdoeNumber;
            secondTempmdoeNumber = mdoeNumber;
        }
        Debug.Log(mdoeNumber);
        yield return new WaitForSeconds(modeIntervalTime);
        modeOpen = true;
    }

    //A點射擊模式
    void Shoot1Above() 
    {
        AudioManager.Instance.PlaySnailBoss("FiringRpgA");
        float bulletRotationRange = Random.Range(15,45);
        float radians = bulletRotationRange * Mathf.PI / 180f;

        if(MoveToLeft) 
        {
            Quaternion bulletRotation = Quaternion.Euler(0f,0f,-1 * bulletRotationRange);
            Vector2 bullectVelocity = new Vector2(-1f,Mathf.Sin(radians)).normalized * bulletSpeed;
            GameObject bullet = Instantiate(bulletPrefab, A.position,bulletRotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullectVelocity;
        }

        else 
        {
            Quaternion bulletRotation = Quaternion.Euler(0f,0f,bulletRotationRange);
            Vector2 bullectVelocity = new Vector2(1f,Mathf.Sin(radians)).normalized * bulletSpeed;
            GameObject bullet = Instantiate(bulletPrefab, A.position,bulletRotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullectVelocity;
        }
    }
    
    //B點射擊模式
    void ShootUnder() 
    {
        AudioManager.Instance.PlaySnailBoss("FiringRpgB");

        float bulletRotationRange = Random.Range(0,10);
        float radians = bulletRotationRange * Mathf.PI / 180f;
        if(MoveToLeft) 
        {
            Quaternion bulletRotation = Quaternion.Euler(0f,0f,-1 * bulletRotationRange);
            Vector2 bullectVelocity = new Vector2(-1f,Mathf.Sin(radians)).normalized * bulletSpeed;
            GameObject bullet = Instantiate(bulletPrefab, B.position,bulletRotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullectVelocity;
        }

        else 
        {
            Quaternion bulletRotation = Quaternion.Euler(0f,0f,bulletRotationRange);
            Vector2 bullectVelocity = new Vector2(1f,Mathf.Sin(radians)).normalized * bulletSpeed;
            GameObject bullet = Instantiate(bulletPrefab, B.position,bulletRotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullectVelocity;
        }
    }
    void Flip()
    {
        if(MoveToLeft) transform.position = new Vector3(transform.position.x + 8f,transform.position.y,transform.position.z);
        else transform.position = new Vector3(transform.position.x -8f,transform.position.y,transform.position.z);
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;

        MoveToLeft = !MoveToLeft;             
    }

}

