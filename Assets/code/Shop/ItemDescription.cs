using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    public Image ItemImage;
    public TMP_Text itemName;
    public TMP_Text itemInfo;
    

    private void Awake()
    {
        ItemImage = transform.Find("ItemImage").GetComponent<Image>();
        itemName= transform.Find("ItemName").GetComponent<TMP_Text>();
        itemInfo = transform.Find("ItemInfo").GetComponent<TMP_Text>();
    }
    public void GetItemInfo(Image image,string name, string info)
    {
        Debug.Log(name);
        ItemImage.sprite = image.sprite;
        itemName.SetText(name);
        itemInfo.SetText(info);
    }
}
