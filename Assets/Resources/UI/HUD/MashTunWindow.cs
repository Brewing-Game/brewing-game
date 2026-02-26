using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MashTunWindow : MonoBehaviour
{
    public MashTun tun;

    public Button fillButton;
    public TMP_Text fillButtonLabel;
    public Button upgradeButton;
    public Button brewButton;
    public Button collectButton;
    public Slider waterLevel;  
    public TMP_InputField inputStopLevel;

    [Header("Upgrade")]
    [SerializeField] private int upgradeCost = 10;
    
    void OnEnable()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        gm.OnPointsChanged += HandlePointsChanged;

        // IMPORTANT: sync immediately when the window becomes enabled
        RefreshUpgradeButton(gm.totalPoints);
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnPointsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(int newPoints)
    {
        Debug.Log($"[MashTunWindow] Points changed => {newPoints}");
        RefreshUpgradeButton(newPoints);
    }

    private void RefreshUpgradeButton(int points)
    {
        Debug.Log($"[MashTunWindow] RefreshUpgradeButton points={points} cost={upgradeCost}");
        if (upgradeButton == null) 
        {
            Debug.Log("Upgrade button is null");
            return;
        }

        // upgrade button has already been used, do nothing.
        if (!upgradeButton.gameObject.activeSelf) return;

        upgradeButton.interactable = points >= upgradeCost;
    }

    
    public void OnToggleFill()
    {
        if(!tun.isFilling)
        {
            Debug.Log("Clicked fill button");
            tun.StartWaterFlow();
            //change fillbutton text to "stop"
            fillButtonLabel.text = ("Stop");            
        }
        else
        {
            Debug.Log("Clicked stop button");            
            tun.StopWaterFlow();
            fillButtonLabel.text = ("Fill");            
        }        
    }

    public void OnBrewButtonClick()
    {
        if(tun.waterLevel > 0)
        {
            tun.BrewBeer();
        }        
    }

    public void OnCollectButtonClick()
    {
        int points = tun.CollectBeer();
        if(points > 0)
        {
            Debug.Log($"Points collected {points}");

            //notify gamemanager
            if(GameManager.Instance != null)
            {
                GameManager.Instance.OnBeerCollected(points);
            }
        }
        else
        {
            Debug.Log($"You obtained no points: {points}");
        }
    }

    public void OnUpgradeButtonClick()
    {
        if (GameManager.Instance != null && GameManager.Instance.totalPoints < upgradeCost)
        return;

        IInstrument waterLevelSensor = new WaterLevelSensor(waterLevel);
        IInstrument autoStopValve = new AutoStopValve();
        tun.AddInstrument(waterLevelSensor);
        tun.AddInstrument(autoStopValve);
        upgradeButton.gameObject.SetActive(false);        
    }
    
    public void UpdateUI()
    {
        if (tun == null) return;

        if (fillButtonLabel == null || upgradeButton == null || waterLevel == null) return;

        var viewModel = tun.GetViewModel();
        fillButtonLabel.text = viewModel.FillButtonLabel;
        upgradeButton.gameObject.SetActive(viewModel.ShowUpgradeButton);
        waterLevel.gameObject.SetActive(viewModel.ShowWaterLevelSlider);
        waterLevel.value = viewModel.WaterLevelPercentage;
    }
    void Awake()
    {
        if (fillButtonLabel == null && fillButton != null)
            fillButtonLabel = fillButton.GetComponentInChildren<TMP_Text>(true);
            
        fillButtonLabel = fillButton.GetComponentInChildren<TMP_Text>(true);        
        fillButton.onClick.AddListener(OnToggleFill);
        brewButton.onClick.AddListener(OnBrewButtonClick);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        collectButton.onClick.AddListener(OnCollectButtonClick);
    }
    void Start()
    {   
        
    }

    void Update()
    {           
        UpdateUI();           
        
        if(Input.GetKeyDown(KeyCode.F))
            OnToggleFill();
        if(Input.GetKeyDown(KeyCode.B))
            OnBrewButtonClick();
        if(Input.GetKeyDown(KeyCode.C))
            OnCollectButtonClick(); 
       
    }
}
