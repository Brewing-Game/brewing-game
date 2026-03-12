using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;

// This instrument provides a preview of the infill level on each MashTun. 
public class WaterLevelSensor : Instrument
{
    MashTun mashTun;
    [NonSerialized]
    public Slider waterLevelSlider;
    

    void Start()
    {
        
    }

    public override void OnMashTunFill()
    {
        if (waterLevelSlider != null && mashTun != null)
        {
            waterLevelSlider.value = mashTun.waterLevel / mashTun.maxWaterLevel;
        }
    }

    public override void OnBrewing()
    {        
        
    }

    public override void OnCollectBeer()
    {        
        if(waterLevelSlider != null)
        {            
            waterLevelSlider.value = 0;
        }
    }

    public override void UpdateViewModel(MashTunViewModel viewModel, MashTun tun)
    {        
        viewModel.ShowWaterLevelSlider = true;
    }
    public override GameObject GetUIElement()
    {
        return waterLevelSlider != null ? waterLevelSlider.gameObject : null;
    }
    public override void Install(MashTun tun)
    {
        mashTun = tun;
        if(waterLevelSlider != null)
        {
            waterLevelSlider.gameObject.SetActive(true);
            waterLevelSlider.minValue = 0;
            waterLevelSlider.maxValue = 1;
            waterLevelSlider.value = 0;
        }
        Debug.Log("WaterLevelSensor installed");
    }
    public override void Uninstall()
    {
        if(waterLevelSlider != null)
        {
            waterLevelSlider.gameObject.SetActive(false);
        }
        mashTun = null;
        Debug.Log("WaterLevelSensor uninstalled");
    }
}