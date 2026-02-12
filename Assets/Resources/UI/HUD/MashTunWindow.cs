using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MashTunWindow : MonoBehaviour
{
    public MashTun tun;

    public Button fillButton;
    public TMP_Text fillButtonLabel;
    public Button upgradeButton;
    public Button brewButton;
    public Button collectButton;
    public Slider waterLevel;  
    public TMP_InputField inputStopLevel;  
    public void OnToggleFill()
    {
        if(!tun.isFilling)
        {
            Debug.Log("Clicked fill button");
            tun.StartWaterFlow();
            //change fillbutton text to "stop"
            fillButtonLabel.text = ("Stop");            
        }
        else
        {
            Debug.Log("Clicked stop button");            
            tun.StopWaterFlow();
            fillButtonLabel.text = ("Fill");            
        }        
    }

    public void OnBrewButtonClick()
    {
        if(tun.waterLevel > 0)
        {
            tun.BrewBeer();
        }        
    }

    public void OnCollectButtonClick()
    {
        int points = tun.CollectBeer();
        if(points > 0)
        {
            Debug.Log($"Points collected {points}");

            //notify gamemanager
            if(GameManager.Instance != null)
            {
                GameManager.Instance.OnBeerCollected(points);
            }
        }
    }

    public void OnUpgradeButtonClick()
    {
        IInstrument waterLevelSensor = new WaterLevelSensor(waterLevel);
        IInstrument autoStopValve = new AutoStopValve();
        tun.AddInstrument(waterLevelSensor);
        tun.AddInstrument(autoStopValve);
        upgradeButton.gameObject.SetActive(false);        
    }
    
    public void UpdateUI()
    {
        if (tun == null) return;
        var viewModel = tun.GetViewModel();
        fillButtonLabel.text = viewModel.FillButtonLabel;
        upgradeButton.gameObject.SetActive(viewModel.ShowUpgradeButton);
        waterLevel.gameObject.SetActive(viewModel.ShowWaterLevelSlider);
        waterLevel.value = viewModel.WaterLevelPercentage;
    }
    void Start()
    {   
        fillButtonLabel = fillButton.GetComponentInChildren<TMP_Text>(true);
        Debug.Log(fillButtonLabel);
        fillButton.onClick.AddListener(OnToggleFill);
        brewButton.onClick.AddListener(OnBrewButtonClick);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        collectButton.onClick.AddListener(OnCollectButtonClick);
    }

    void Update()
    {
       // Debug.Log(tun.waterLevel);
       UpdateUI();
    }
}
