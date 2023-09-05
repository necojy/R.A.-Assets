using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject Prefabs;
    int number = 5;
    Queue<GameObject> pool = new Queue<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < number; i++)
        {
            GameObject Object = Instantiate(Prefabs, transform);
            pool.Enqueue(Object);
            Object.SetActive(false);
        }
    }
    public void ReUse(Vector3 pos)
    {
        if (pool.Count > 0)
        {
            GameObject reuse = pool.Dequeue();
            reuse.transform.position = pos;
            reuse.transform.position = pos;
            reuse.SetActive(true);
        }
        else
        {
            GameObject reuse = Instantiate(Prefabs, transform);
            reuse.transform.position = pos;
        }
    }
    public void ReUse()
    {
        if (pool.Count > 0)
        {
            GameObject reuse = pool.Dequeue();
            reuse.SetActive(true);
        }
        else
        {
            GameObject reuse = Instantiate(Prefabs, transform);
        }
    }
    public void Recovery(GameObject recovery)
    {
        pool.Enqueue(recovery);
        recovery.SetActive(false);
    }
}
