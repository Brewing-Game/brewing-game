using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UltrasonicFlowMeter : Instrument
{
    private MashTun mashTun;    
    private Coroutine _stopAtOptimalLevelCoroutine;
    [NonSerialized]public Slider optimalLevelMarker;   
    private const float _pointsMultiplier = 2f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StopAtOptimalLevel()
    {
        float targetLevel = mashTun.optimalWaterLevel;
        while (mashTun != null && mashTun.isFilling)
        {
            if (mashTun.waterLevel >= targetLevel)
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
            _stopAtOptimalLevelCoroutine = mashTun.StartCoroutine(StopAtOptimalLevel());
        }
        if(mashTun != null && !mashTun.isFilling && _stopAtOptimalLevelCoroutine != null)
        {
            mashTun.StopCoroutine(_stopAtOptimalLevelCoroutine);
            _stopAtOptimalLevelCoroutine = null;
        }
    }
    public override void OnBrewing(){}
    public override void OnCollectBeer(){}
    public override GameObject GetUIElement(){ return null;}
    public override void Install(MashTun tun)
    {
         mashTun = tun;
        if (optimalLevelMarker != null)
        {
            optimalLevelMarker.minValue = 0;
            optimalLevelMarker.maxValue = 1;
            optimalLevelMarker.value = tun.optimalWaterLevel / tun.maxWaterLevel;
        }
        Debug.Log("UltrasonicFlowMeter installed");
    }
    public override void Uninstall()
    {
        if (_stopAtOptimalLevelCoroutine != null && mashTun != null)
        {
            mashTun.StopCoroutine(_stopAtOptimalLevelCoroutine);
            _stopAtOptimalLevelCoroutine = null;
        }

        mashTun = null;
        Debug.Log("Ultrasonic Flow Meter uninstalled");
    }
    public override void UpdateViewModel(MashTunViewModel viewModel, MashTun tun)
    {
        viewModel.ShowOptimalLevelMarker = true;
        viewModel.OptimalLevel = tun.optimalWaterLevel / tun.maxWaterLevel;
        viewModel.PointsMultiplier = _pointsMultiplier;
        
        if (optimalLevelMarker != null)
        {
            optimalLevelMarker.minValue = 0;
            optimalLevelMarker.maxValue = 1;
            optimalLevelMarker.value = tun.optimalWaterLevel / tun.maxWaterLevel;
        }
    }
}
