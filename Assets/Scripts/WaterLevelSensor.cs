using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelSensor : IInstrument
{
    MashTun mashTun;
    private Slider _waterLevelSlider;

    public WaterLevelSensor(Slider sliderUI)
    {
        this._waterLevelSlider = sliderUI;
        if (_waterLevelSlider != null)
        {
            _waterLevelSlider.gameObject.SetActive(false);
        }
    }
    public void OnMashTunFill()
    {
        if(_waterLevelSlider != null && mashTun != null)
        {
            _waterLevelSlider.value = mashTun.waterLevel / mashTun.maxWaterLevel;
        }
    }
    public void OnBrewing()
    {
        //slider could have animation or water can change color
    }
    public void OnCollectBeer()
    {
        if(_waterLevelSlider != null)
        {
            _waterLevelSlider.value = 0;
        }
    }
    public GameObject GetUIElement()
    {
        return _waterLevelSlider != null ? _waterLevelSlider.gameObject : null;
    }
    public void Install(MashTun tun)
    {
        mashTun = tun;
        if(_waterLevelSlider != null)
        {
            _waterLevelSlider.gameObject.SetActive(true);
            _waterLevelSlider.minValue = 0;
            _waterLevelSlider.maxValue = 1;
            _waterLevelSlider.value = 0;
        }
        Debug.Log("WaterLevelSensor installed");
    }
    public void Uninstall()
    {
        if(_waterLevelSlider != null)
        {
            _waterLevelSlider.gameObject.SetActive(false);
        }
        mashTun = null;
        Debug.Log("WaterLevelSensor uninstalled");
    }
    
}
