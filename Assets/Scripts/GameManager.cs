using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleton pattern using static to insure just one game manager
    public static GameManager Instance { get; set; }

    [Header("Game State")]
    public bool isGameActive = false;
    private int _totalPoints = 0;
    private int _brewsCompleted = 0;

    [Header("Win Condition")]
    [SerializeField] private int _brewsToWin = 3;
    [SerializeField] private int _pointsToWin = 100;

    [Header("UI References")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TMP_Text _winMessageText;
    [SerializeField] private TMP_Text _pointsDisplayText;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        isGameActive = true;
        _totalPoints = 0;
        _brewsCompleted = 0;
        if(_winPanel != null)
        {
            _winPanel.SetActive(false);
        }
        UpdatePointsDisplay();
        Debug.Log("Game Started");
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
