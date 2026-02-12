using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AutoStopValve : IInstrument
{
    private MashTun mashTun;
    [SerializeField] private float _setStopLevel;
    private Coroutine _stopAtSetLevelCoroutine;
    private TMP_InputField _inputLevel;
    private GameObject UIElement;
    [SerializeField] private bool _isStopLevelSet;

    public AutoStopValve(TMP_InputField inputFieldUI)
    {
        this._inputLevel = inputFieldUI;
        this.UIElement = inputFieldUI?.gameObject;
        this._setStopLevel = 7f;
        this._isStopLevelSet = false;

        if(_inputLevel != null)
        {            
            _inputLevel.onEndEdit.AddListener(OnInputValueChanged);
        }
    }

    private void OnInputValueChanged(string value)
    {   
        if (float.TryParse(value, out float result))
        {
            _setStopLevel = Mathf.Clamp(result, 0f, mashTun?.maxWaterLevel ?? 100f);
            _isStopLevelSet = true;

            if (_inputLevel != null)
            {
                _inputLevel.text = _setStopLevel.ToString("F1");
            }
            Debug.Log($"AutoStopValve set to stop at: {_setStopLevel}");
        }
        else if (string.IsNullOrEmpty(value))
        {            
            _isStopLevelSet = false;
            _setStopLevel = 7f;
            Debug.Log("AutoStopValve disabled (no level set)");
        }
    }

    private IEnumerator StopAtSetLevel()
    {
        while (mashTun != null && mashTun.isFilling)
        {
            if(mashTun.waterLevel >= _setStopLevel)
            {
                mashTun.StopWaterFlow();
                _stopAtSetLevelCoroutine = null;
                yield break; //exit coroutine
            }
            yield return null;
        }
    }
    public void OnMashTunFill()
    {
        if(mashTun != null && mashTun.isFilling && _stopAtSetLevelCoroutine == null)
        {
            _stopAtSetLevelCoroutine = mashTun.StartCoroutine(StopAtSetLevel());
        }
        if(mashTun != null && !mashTun.isFilling && _stopAtSetLevelCoroutine != null)
        {
            mashTun.StopCoroutine(_stopAtSetLevelCoroutine);
            _stopAtSetLevelCoroutine = null;
        }
    }
    public void OnBrewing()
    {
        if (_stopAtSetLevelCoroutine != null && mashTun != null)
        {
            mashTun.StopCoroutine(_stopAtSetLevelCoroutine);
            _stopAtSetLevelCoroutine = null;
        }
    }
    public void OnCollectBeer()
    {
        if (_inputLevel != null)
        {
            _inputLevel.text = "";
        }
        _setStopLevel = 7f;
    }
    public GameObject GetUIElement()
    {
        return UIElement;
    }
    public void Install(MashTun tun)
    {
        mashTun = tun;
        if (_inputLevel != null)
        {
            _inputLevel.gameObject.SetActive(true);
            _inputLevel.text = "";
            _inputLevel.placeholder.GetComponent<TMP_Text>().text = "Stop Level";
        }
        Debug.Log("AutoStopValve installed");
    }
    public void Uninstall()
    {
        if (_stopAtSetLevelCoroutine != null && mashTun != null)
        {
            mashTun.StopCoroutine(_stopAtSetLevelCoroutine);
            _stopAtSetLevelCoroutine = null;
        }
        
        if (_inputLevel != null)
        {
            _inputLevel.onEndEdit.RemoveListener(OnInputValueChanged);
            _inputLevel.gameObject.SetActive(false);
        }
        
        _isStopLevelSet = false;
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
