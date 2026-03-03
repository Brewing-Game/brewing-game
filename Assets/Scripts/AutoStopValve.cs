using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AutoStopValve : Instrument
{
    private MashTun mashTun;    
    private Coroutine _stopAtOptimalLevelCoroutine;
    private float _optimalStopLevel;
  
    private IEnumerator StopAtSetLevel()
    {
        while (mashTun != null && mashTun.isFilling)
        {
            _optimalStopLevel = mashTun.optimalWaterLevel;
            if(mashTun.waterLevel >= _optimalStopLevel)
            {
                mashTun.StopWaterFlow();
                _stopAtOptimalLevelCoroutine = null;
                yield break; //exit coroutine
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

        Debug.Log("AutoStopValve installed");
    }
    public override void Uninstall()
    {
        if (_stopAtOptimalLevelCoroutine != null && mashTun != null)
        {
            mashTun.StopCoroutine(_stopAtOptimalLevelCoroutine);
            _stopAtOptimalLevelCoroutine = null;
        }

        mashTun = null;
        Debug.Log("AutoStopValve uninstalled");
    }

    public override void UpdateViewModel(MashTunViewModel viewModel, MashTun tun){}
}
