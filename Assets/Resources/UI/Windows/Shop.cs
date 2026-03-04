using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

// This class represents and in-game store. It is used to purchase "upgrades" or "instruments",
// When added to scene, the "Requirements" fields must be populated to ensure proper functionality.
public class Shop : MonoBehaviour
{
    [Header("External Requirements")]
    public PlayerScoreIndicator playerScoreIndicator; 
    [Header("Upgrade Requirements")]
    public Slider waterLevelSlider;

    [Header("Stock")]
    public GameObject itemsContainer;
    public GameObject[] itemsToSell;

    [NonSerialized]
    public GameObject[] _mashTuns;

    [Header("Item Preview")]
    public GameObject sidePanel;
    public TextMeshProUGUI priceTag;
    public TextMeshProUGUI description;
    public Image itemThumbnail;
    public TextMeshProUGUI itemName;
    public Button purchaseItemButton;
    public GameObject soldBanner;

    [NonSerialized]
    public ShopInstrument selectedItem;
    

    void Start()
    {
        _mashTuns = GameObject.FindGameObjectsWithTag("MashTun");

        foreach ( GameObject instrument in itemsToSell)
        {
            GameObject instantiatedInstrument = Instantiate(instrument);
            instantiatedInstrument.GetComponent<ShopInstrument>().shop = this;
            instantiatedInstrument.transform.SetParent(itemsContainer.transform);
        }
    }

    void Update()
    {
        if (!selectedItem)
        {
            sidePanel.SetActive(false);
            return;
        }

        sidePanel.SetActive(true);

        if (selectedItem.isSold) 
        {
            purchaseItemButton.interactable = false;
            soldBanner.SetActive(true);
        }
        else
        {
            purchaseItemButton.interactable = true;
            soldBanner.SetActive(false);
        }
    }

    public void TryPurchase()
    {
        if (!selectedItem) return;

        if (playerScoreIndicator.score >= selectedItem.price)
        {
            playerScoreIndicator.score -= selectedItem.price;
            foreach (GameObject mashtun in _mashTuns)
            {
                mashtun.GetComponent<MashTun>().AddInstrument(selectedItem.instrumentType);
            }
            selectedItem.isSold = true;
            FullfillInstrumentRequirements();
        }   
    }

    // 
    public void FullfillInstrumentRequirements()
    {
        if (!selectedItem) return;

        if (selectedItem.instrumentType is WaterLevelSensor)
        {
            selectedItem.gameObject.GetComponent<WaterLevelSensor>().waterLevelSlider = waterLevelSlider;
            return;
        } 
    }
}
