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
    public Slider selectedLevelSlider;
    public TMP_InputField inputStopLevel;
    public Slider optimalLevelMarker;

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
            GameManager.Instance.SpendPoints(selectedItem.price);

            foreach (GameObject mashTunObj in _mashTuns)
            {
                MashTun tun = mashTunObj.GetComponent<MashTun>();
                Instrument newInstance = selectedItem.gameObject.AddComponent(selectedItem.instrumentType.GetType()) as Instrument;
                tun.AddInstrument(newInstance);
                FulfillRequirementsForInstance(newInstance);
            }

            selectedItem.isSold = true;
        }   
    }

    private void FulfillRequirementsForInstance(Instrument instrument)
    {
        if (instrument is SightGlass sightGlass)
        {
            sightGlass.waterLevelSlider = waterLevelSlider;
            return;
        }
        if (instrument is FloatSwitch floatSwitch)
        {
            floatSwitch.selectedLevelSlider = selectedLevelSlider;
            return;
        }
        if (instrument is UltrasonicFlowMeter ultrasonicFlowMeter)
        {
            ultrasonicFlowMeter.optimalLevelMarker = optimalLevelMarker;
            return;
        }
    }
}
