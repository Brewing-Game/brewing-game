using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

// This class represents a purchasable store item.
// It requires you to also attach the item's Instrument class to this.gameObject.
[RequireComponent(typeof(Instrument))]
public class ShopInstrument : MonoBehaviour
{
    public TextMeshProUGUI priceTag;
    public TextMeshProUGUI nameTag;
    public Image imageComp;
    public Sprite itemSprite;

    public int price = 0;
    public GameObject soldBanner;

    public string description;
    public string itemName;

    [NonSerialized]
    public Instrument instrumentType;

    [NonSerialized]
    public bool isSold;
    private bool wasSold;

    [NonSerialized]
    public Shop shop;
    

    void Start()
    {
        instrumentType = GetComponent<Instrument>();
        priceTag.text = price.ToString();
        nameTag.text = itemName;

        if (!itemSprite) itemSprite = imageComp.sprite;
    }

    void Update()
    {
        if (isSold && !wasSold)
        {
            wasSold = true;
            soldBanner.SetActive(true);
        }
    }

    public void Select()
    {
        shop.description.text = description;
        shop.itemName.text = itemName;
        shop.priceTag.text = price.ToString();
        shop.itemThumbnail.sprite = itemSprite;
        shop.selectedItem = this;
    }
}
