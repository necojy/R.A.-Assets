using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStack : MonoBehaviour
{
    public GameObject Prefabs;
    int top = 0;
    List<GameObject> pool = new List<GameObject>();

    public void add(float xmove)
    {
        if (top < pool.Count)
        {
            pool[top].SetActive(true);
        }
        else
        {
            GameObject Object = Instantiate(Prefabs, transform);
            if (pool.Count > 0)
                Object.transform.position =
                new Vector3(
                    pool[pool.Count - 1].transform.position.x + xmove,
                    Object.transform.position.y,
                    Object.transform.position.z);
            pool.Add(Object);
        }
        top++;
    }
    public void remove()
    {
        if (top > 0)
            pool[--top].SetActive(false);
        else Debug.Log("error");
    }
    public int get_top()
    {
        return top;
    }
}
