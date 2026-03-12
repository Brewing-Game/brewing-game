using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FloatSwitch : Instrument
{
    private MashTun mashTun;    
    private Coroutine _stopAtOptimalLevelCoroutine;
    private float _selectedStopLevel;
    public float selectedLevel => _selectedStopLevel;
    public TMP_InputField inputLevel;
    [NonSerialized]
    public Slider selectedLevelSlider;
    
    public void SetSelectedLevel(float value)
    {
        _selectedStopLevel = value;
        if(selectedLevelSlider != null)
        {
            selectedLevelSlider.value = _selectedStopLevel;
        }
    }

    private void OnInputLevelChanged(string value)
    {
        if(float.TryParse(value, out float parsed))
        {
            _selectedStopLevel = parsed;
            if(selectedLevelSlider != null)
                selectedLevelSlider.value = _selectedStopLevel;
        }
    }
    private IEnumerator StopAtSetLevel()
    {        
        float variance = (UnityEngine.Random.value * 20f) - 10f;
        float targetLevel = _selectedStopLevel + variance;
        Debug.Log($"FloatSwitch targeting: {targetLevel} (selected: {_selectedStopLevel}, variance: {variance})");
        while (mashTun != null && mashTun.isFilling)
        {            
            if(mashTun.waterLevel >= targetLevel)
            {
                mashTun.StopWaterFlow();
                _stopAtOptimalLevelCoroutine = null;
                yield break;
            }
            yield return null;
        }
    }
    public override void OnMashTunFill()
    {
        if(mashTun != null && mashTun.isFilling && _stopAtOptimalLevelCoroutine == null)
        {
            _stopAtOptimalLevelCoroutine = mashTun.StartCoroutine(StopAtSetLevel());
        }
        if(mashTun != null && !mashTun.isFilling && _stopAtOptimalLevelCoroutine != null)
        {
            mashTun.StopCoroutine(_stopAtOptimalLevelCoroutine);
            _stopAtOptimalLevelCoroutine = null;
        }
    }
    public override void OnBrewing()
    {
        if (_stopAtOptimalLevelCoroutine != null && mashTun != null)
        {
            mashTun.StopCoroutine(_stopAtOptimalLevelCoroutine);
            _stopAtOptimalLevelCoroutine = null;
        }
    }

    public override void OnCollectBeer()
    {
        
    }

    public override GameObject GetUIElement()
    {
        return null;
    }
    public override void Install(MashTun tun)
    {
        mashTun = tun;
        if(selectedLevelSlider != null)
        {
            selectedLevelSlider.minValue = 0;
            selectedLevelSlider.maxValue = tun.maxWaterLevel;
            selectedLevelSlider.value = _selectedStopLevel;
        }
        Debug.Log("FloatSwitch installed");
    }
    public override void Uninstall()
    {
        if (_stopAtOptimalLevelCoroutine != null && mashTun != null)
        {
            mashTun.StopCoroutine(_stopAtOptimalLevelCoroutine);
            _stopAtOptimalLevelCoroutine = null;
        }

        mashTun = null;
        Debug.Log("FloatSwitch uninstalled");
    }

    public override void UpdateViewModel(MashTunViewModel viewModel, MashTun tun)
    {
        viewModel.ShowSelectedLevelInput = true;
        viewModel.SelectedLevel = _selectedStopLevel;
    }
}
