using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDeadMySelf : MonoBehaviour
{
    public GameObject DeadMySelf;//生成物件
    public Transform[] generatePos;//生成位置
    public float spawnInterval = 2f;//生成間隔時間
    private bool isEnter = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("'enter'");
        // 啟動生成協程
        if (!isEnter) Destroy(GetComponent<BoxCollider2D>());
        if (!isEnter) StartCoroutine(SpawnObjects());
        isEnter = true;
    }
    private IEnumerator SpawnObjects()
    {
        for (int i = 0; i < generatePos.Length; i++)
        {
            //使用生成的隨機位置生成物體
            Instantiate(DeadMySelf, generatePos[i].position, Quaternion.identity);

            //等待指定的時間間隔
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
