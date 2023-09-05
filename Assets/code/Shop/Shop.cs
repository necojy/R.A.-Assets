using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    public int itemAmount;
    public int itemIndex;
    public bool isshow;
    PlayerMove playerMove;
    PlayerAttack playerAttack;
    
    private void Awake()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        itemAmount = content.childCount;
    }
    public void Show()
    {
        isshow = true;
        gameObject.SetActive(true);
        //StartCoroutine(DelayedSelectChild(0));
        SelectChild(0);
        Time.timeScale = 0;
        playerMove.isInputEnabled = false;
        playerAttack.isAttackable = false;
    }
    public void Hide()
    {
        isshow = false;
        gameObject.SetActive(false);
        Time.timeScale = 1;
        playerMove.isInputEnabled = true;
        playerAttack.isAttackable = true;
    }
    public void SelectChild(int index)
    {

        GameObject childobj = content.transform.GetChild(index).gameObject;
        ItemButton item = childobj.GetComponent<ItemButton>();
        item.ObtainSelectionFocuse();
    }
    public IEnumerator DelayedSelectChild(int index)
    {
        yield return new WaitForSeconds(1f);
        SelectChild(index);
    }
}
