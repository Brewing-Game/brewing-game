using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AutoStopValve : IInstrument
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
    public void OnMashTunFill()
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
    public void OnBrewing()
    {
        if (_stopAtOptimalLevelCoroutine != null && mashTun != null)
        {
            mashTun.StopCoroutine(_stopAtOptimalLevelCoroutine);
            _stopAtOptimalLevelCoroutine = null;
        }
    }

    public void OnCollectBeer()
    {
        
    }

    public GameObject GetUIElement()
    {
        return null;
    }
    public void Install(MashTun tun)
    {
        mashTun = tun;

        Debug.Log("AutoStopValve installed");
    }
    public void Uninstall()
    {
        if (_stopAtOptimalLevelCoroutine != null && mashTun != null)
        {
            mashTun.StopCoroutine(_stopAtOptimalLevelCoroutine);
            _stopAtOptimalLevelCoroutine = null;
        }

        mashTun = null;
        Debug.Log("AutoStopValve uninstalled");
    }

    public void UpdateViewModel(MashTunViewModel viewModel, MashTun tun){}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
