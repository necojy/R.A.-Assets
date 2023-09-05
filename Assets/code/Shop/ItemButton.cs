using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using static Item;
using Unity.VisualScripting;
using System;
using Unity.Burst.CompilerServices;


public class ItemButton : MonoBehaviour, ISelectHandler
{
    private Item.ItemType itemtype;
    public Image itemImage;
    public string itemName;
    public TMP_Text itemPrice;
    public string itemInfo;
    

    ItemDescription itemDescription;
    [System.Serializable]
    public class ItemButtonEvent : UnityEvent<ItemButton> { }
    [SerializeField] private ItemButtonEvent onSelectEvent;
    public int moneyAmount = 10;
    public int healthpotionAmount = 0;
    Shop shop;
    public ItemButtonEvent OnSelectEvent { get => onSelectEvent; set => onSelectEvent = value; }

    private void Awake()
    {
        itemName = transform.Find("NameText").GetComponent<TMP_Text>().text.ToString();
        itemPrice = transform.Find("PriceText").GetComponent<TMP_Text>();
        itemtype = (Item.ItemType)Enum.Parse(typeof(Item.ItemType), itemName);
        itemPrice.SetText("Price:" + Item.GetCost(itemtype).ToString());
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<ItemDescription>();
        shop = GameObject.Find("Shop").GetComponent<Shop>();

    }
    private void Update()
    {

        if (shop.itemIndex == shop.itemAmount - 1)
        {
            StartCoroutine(Wait());
        }
        if (Input.GetAxisRaw("Vertical") < 0 && shop.itemIndex >= shop.itemAmount) 
        {
            Debug.Log("down");
            shop.SelectChild(0);
            
        }   
        

        if (shop.itemIndex == 0)
        {
            StartCoroutine(Wait());
        }
        if (Input.GetAxisRaw("Vertical") > 0 && shop.itemIndex < 0)
        {
            Debug.Log("up");
            shop.SelectChild(shop.itemAmount - 1);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        if (shop.itemIndex == shop.itemAmount - 1)
            shop.itemIndex=shop.itemAmount;
        if (shop.itemIndex == 0)
            shop.itemIndex = -1;
    }
    public void TryBuyItem()
    {
        if (TrySpendMoneyAmount(Item.GetCost(itemtype)))
        {
            BoughtItem(itemtype);
        }
    }
    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought Item:" + itemType);
        switch (itemType)
        {
            case Item.ItemType.HealthPotion: healthpotionAmount += 1; break;
            case Item.ItemType.Skill: Debug.Log("Get Skill"); break;
        }


    }
    public bool TrySpendMoneyAmount(int spendmoneyAmount)
    {
        if (moneyAmount >= spendmoneyAmount)
        {
            moneyAmount -= spendmoneyAmount;
            Debug.Log("spend " + spendmoneyAmount);
            return true;
        }
        else
        {
            Debug.Log("money not enough");
            return false;
        }
    }

    public void ObtainSelectionFocuse()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        onSelectEvent.Invoke(this);
        itemDescription.GetItemInfo(itemImage, itemName, itemInfo);
    }
    public void OnSelect(BaseEventData eventData)
    {
        itemDescription.GetItemInfo(itemImage, itemName, itemInfo);
        shop.itemIndex = transform.GetSiblingIndex();

    }
}
