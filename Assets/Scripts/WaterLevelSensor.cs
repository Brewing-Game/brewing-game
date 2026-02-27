using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelSensor : IInstrument
{
    MashTun mashTun;
    private Slider _waterLevelSlider;
    private Image _fillImage;

    private Color waterColor = new Color(0.3f, 0.5f, 0.9f);
    private Color beerColor = new Color(0.95f, 0.75f, 0.2f);
    private Coroutine _colorTransitionCoroutine;

    private Color _currentColor;
    
    public WaterLevelSensor(Slider sliderUI)
    {
        this._waterLevelSlider = sliderUI;
        this._currentColor = waterColor;
        if (_waterLevelSlider != null)
        {
            _waterLevelSlider.gameObject.SetActive(false);
            _fillImage = _waterLevelSlider.fillRect.GetComponent<Image>();
            if (_fillImage != null)
            {
                _fillImage.color = waterColor;
            }
        }
    }
    public void OnMashTunFill()
    {
        if(_waterLevelSlider != null && mashTun != null)
        {
            _waterLevelSlider.value = mashTun.waterLevel / mashTun.maxWaterLevel;
            if (_fillImage != null)
            {
                _fillImage.color = _currentColor;
            }
        }
    }
    public void OnBrewing()
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

    public void OnCollectBeer()
    {
        _currentColor = waterColor;
        if(_waterLevelSlider != null)
        {
            if (_fillImage != null)
            {
                _fillImage.color = _currentColor;
            }
            _waterLevelSlider.value = 0;
        }
    }

    public void UpdateViewModel(MashTunViewModel viewModel, MashTun tun)
    {        
        viewModel.ShowUpgradeButton = false;
        viewModel.ShowWaterLevelSlider = true;
        if (_fillImage != null && _waterLevelSlider != null)
        {
            _fillImage.color = _currentColor;
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