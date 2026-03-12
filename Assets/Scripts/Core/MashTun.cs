using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// This class provides MashTun-specific functionalities.
// Examples include: Brew, Collect, 
public class MashTun : MonoBehaviour
{
    [SerializeField]
    public bool isDebug = false;
    private float _waterLevel;
    public float waterLevel => _waterLevel;
    [SerializeField]
    private float _maxWaterLevel;
    public float maxWaterLevel => _maxWaterLevel;
    private const float _optimalWaterLevel = 70f;
    private const float _tolerance = 30f;
    public float optimalWaterLevel => _optimalWaterLevel;
    [SerializeField]
    public float fillRate = 0.5f;
    private List<Instrument> instruments = new List<Instrument>();
    public MashTunViewModel viewModel;
    private Color _waterColor = new Color(0.3f, 0.5f, 0.9f);
    private Color _beerColor = new Color(0.95f, 0.75f, 0.2f);
    private Coroutine _colorCoroutine;

    private bool _isFilling = false;
    public bool isFilling => _isFilling;
    private bool _isBrewing = false;
    private bool _hasBrewed = false;

    public void StartWaterFlow()
    {
        _isFilling = true;
    }

    public void StopWaterFlow()
    {
        _isFilling = false;
        viewModel.FillButtonLabel = "Fill";        
    }

    public void FillMashTun()
    {
        _waterLevel += fillRate * Time.deltaTime; 
        _waterLevel = Mathf.Min(_waterLevel, _maxWaterLevel);
        viewModel.FillButtonLabel = "Stop";
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
        
        if (_colorCoroutine != null) StopCoroutine(_colorCoroutine);
        _colorCoroutine = StartCoroutine(TransitionColor());

        yield return new WaitForSeconds(5f);
        
        _hasBrewed = true;
        _isBrewing = false;
                
        Debug.Log("Brewing complete");
    }

    private IEnumerator TransitionColor()
    {
        float duration = 5f;
        float elapsed = 0f;
        Color start = viewModel.WaterLevelColor;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            viewModel.WaterLevelColor = Color.Lerp(start, _beerColor, elapsed / duration);
            yield return null;
        }
        viewModel.WaterLevelColor = _beerColor;
    }

    public int CollectBeer()
    {
        if (_hasBrewed)
        {
            int points = (int)Mathf.Round(CalculatePoints(_waterLevel));
            foreach(var instrument in instruments)
            {
                instrument.OnCollectBeer();
            }
            _waterLevel = 0;
            _hasBrewed = false;
            viewModel.WaterLevelColor = _waterColor;

            return points;
        }
        return 0;
    }

    public float CalculatePoints(float level)
    {
        float difference = Mathf.Abs(level - _optimalWaterLevel);
        if(difference >= _tolerance)
        {
            return 0;
        }
        return 100 - (difference * 100/_tolerance);
    }

    public void AddInstrument(Instrument instrument)
    {
        if(!instruments.Contains(instrument))
        {
            instrument.Install(this);
            instruments.Add(instrument);
        }
    }

    public void RemoveInstrument(Instrument instrument)
    {
        if(instruments.Contains(instrument))
        {
            instrument.Uninstall();
            instruments.Remove(instrument);
        }
    }

    public MashTunViewModel GetViewModel()
    {        
        foreach(var instrument in instruments)
        {
            instrument.UpdateViewModel(viewModel, this);
        }

        bool isEmpty = _waterLevel <= 0.0001f;
        bool canBrew = !_isFilling && !_isBrewing && !isEmpty && !_hasBrewed;
        bool canFill = !_isBrewing && !_hasBrewed;         // if brewed, must collect first
        bool canCollect = _hasBrewed && !_isBrewing;

        viewModel.FillButtonLabel = _isFilling ? "Stop" : "Fill";
        viewModel.WaterLevelPercentage = (_maxWaterLevel <= 0f) ? 0f : (_waterLevel / _maxWaterLevel);

        viewModel.isFillButtonInteractible = canFill;
        viewModel.isBrewButtonInteractible = canBrew;
        viewModel.isCollectButtonInteractible = canCollect;

        return viewModel;
    }

    // Start is called before the first frame update
    void Start()
    {
       viewModel = new MashTunViewModel
       {
            FillButtonLabel = _isFilling ? "Stop" : "Fill",
            ShowUpgradeButton = true,
            ShowWaterLevelSlider = false,
            WaterLevelPercentage = _waterLevel / _maxWaterLevel,
            WaterLevelColor = _waterColor,
            isBrewButtonInteractible = false,
            isCollectButtonInteractible = false,
            isFillButtonInteractible = true
       };
    }

    // Update is called once per frame
    void Update()
    {
        if(_isFilling)
        {
            FillMashTun();
        }        
    }
    MashTun mashTun;
}
