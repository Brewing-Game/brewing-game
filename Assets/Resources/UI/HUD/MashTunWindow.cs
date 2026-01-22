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
        Debug.Log(points);
    }

    public void OnUpgradeButtonClick()
    {
        IInstrument waterLevelSensor = new WaterLevelSensor(waterLevel);
        tun.AddInstrument(waterLevelSensor);
        upgradeButton.gameObject.SetActive(false);
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
    }
}
