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
    private Image _fillImage;

    private Color waterColor = new Color(0.3f, 0.5f, 0.9f);
    private Color beerColor = new Color(0.95f, 0.75f, 0.2f);
    public Coroutine _colorTransitionCoroutine;

    private Color _currentColor;

    void Start()
    {
        _currentColor = waterColor;
    }

    public override void OnMashTunFill()
    {
        if(waterLevelSlider != null && mashTun != null)
        {
            waterLevelSlider.value = mashTun.waterLevel / mashTun.maxWaterLevel;
            if (_fillImage != null)
            {
                _fillImage.color = _currentColor;
            }
        }
    }

    public override void OnBrewing()
    {        
        if (_fillImage != null && mashTun != null)
        {
            if (_colorTransitionCoroutine != null)
            {
                mashTun.StopCoroutine(_colorTransitionCoroutine);
            }
            _colorTransitionCoroutine = mashTun.StartCoroutine(TransitionColor());
        }
    }

    private IEnumerator TransitionColor()
    {
        float duration = 5f;
        float elapsed = 0f;
        Color startColor = _currentColor;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            _currentColor = Color.Lerp(startColor, beerColor, t);
            
            yield return null;
        }
        
        _currentColor = beerColor;
        
    }

    public override void OnCollectBeer()
    {
        _currentColor = waterColor;
        if(waterLevelSlider != null)
        {
            if (_fillImage != null)
            {
                _fillImage.color = _currentColor;
            }
            waterLevelSlider.value = 0;
        }
    }

    public override void UpdateViewModel(MashTunViewModel viewModel, MashTun tun)
    {        
        viewModel.ShowUpgradeButton = false;
        viewModel.ShowWaterLevelSlider = true;
        if (_fillImage != null && waterLevelSlider != null)
        {
            _fillImage.color = _currentColor;
        }
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