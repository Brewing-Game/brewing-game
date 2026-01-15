using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MashTun : MonoBehaviour
{
    [SerializeField]
    public bool isDebug = false;
    private float _waterLevel;
    public float WaterLevel => _waterLevel;
    [SerializeField]
    private float _maxWaterLevel;
    public float MaxWaterLevel => _maxWaterLevel;
    [SerializeField]
    private float _optimalWaterLevel;
    private float _fillRate = 0.5f;
    private List<IInstrument> instruments;

    private bool _isFilling = false;
    private bool _isBrewing = false;
    private bool _hasBrewed = false;

    public void StartWaterFlow()
    {
        _isFilling = true;
    }

    public void StopWaterFlow()
    {
        _isFilling = false;
    }

    public void FillMashTun()
    {
        _waterLevel += _fillRate * Time.deltaTime; 
        _waterLevel = Mathf.Min(_waterLevel, _maxWaterLevel);
        foreach(var instrument in instruments)
        {
            instrument.OnMashTunFill();
        }             
    }

    public void BrewBeer()
    {
        if (!_isFilling && !_isBrewing && _waterLevel > 0 && !_hasBrewed)
        {
            StartCoroutine(BrewingProcess());
        }
    }

    private IEnumerator BrewingProcess()
    {
        _isBrewing = true;
        
        foreach(var instrument in instruments)
        {
            instrument.OnBrewing();
        }
        
        Debug.Log("Brewing started");
        
        // TODO add brewing animation
        yield return new WaitForSeconds(5f);
        
        _hasBrewed = true;
        _isBrewing = false;
        
        Debug.Log("Brewing complete");
    }

    public int CollectBeer()
    {
        if (_hasBrewed)
        {
            int points = (int)Mathf.Round(CalculateQualityIndex(_waterLevel) * 1.0f);
            foreach(var instrument in instruments)
            {
                instrument.OnCollectBeer();
            }
            _waterLevel = 0;
            _hasBrewed = false;
            return points;
        }
        return 0;
    }

    public float CalculateQualityIndex(float level)
    {
        return level;
    }

    public void AddInstrument(IInstrument instrument)
    {
        if(!instruments.Contains(instrument))
        {
            instrument.Install(this);
            instruments.Add(instrument);
        }
    }

    public void RemoveInstrument(IInstrument instrument)
    {
        if(instruments.Contains(instrument))
        {
            instrument.Uninstall();
            instruments.Remove(instrument);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instruments = new List<IInstrument>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isFilling)
        {
            FillMashTun();
        }        
    }
}
