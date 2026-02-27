using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //singleton pattern using static to insure just one game manager
    public static GameManager Instance { get; set; }

    public event Action<int> OnPointsChanged; //notify subscribers that points have changed

    [Header("Game State")]
    public bool isGameActive = false;
    private int _totalPoints = 0;
    public int totalPoints => _totalPoints;

    [Header("Win Condition")]   
    [SerializeField] private int _pointsToWin = 1000;
    [SerializeField] private int _startPoints = 0;

    [Header("UI")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TMP_Text _winMessageText;
    [SerializeField] private PlayerScoreIndicator playerScoreIndicator;
    [SerializeField] private MashTunWindow mashTunWindow;
    

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
        _totalPoints = _startPoints;
        playerScoreIndicator.Score = _totalPoints;
        OnPointsChanged?.Invoke(_totalPoints);
        
        if(_winPanel != null)
        {
            _winPanel.SetActive(false);
        }
        
        Debug.Log("Game Started");
    }

    public void OnBeerCollected(int points)
    {
        if(!isGameActive) return;

        _totalPoints += points;        

        if(playerScoreIndicator != null)
        {
            playerScoreIndicator.Score = _totalPoints;
        }
        OnPointsChanged?.Invoke(_totalPoints);
    }

    private void CheckWinCondition()
    {
        if(_totalPoints >= _pointsToWin)
        {
            EndGame(true);
        }  
    }
    private void EndGame(bool isWinConditionMet)
    {
        isGameActive = false;
        if(isWinConditionMet)
        {
            ShowWinScreen();
        }
    }

    private void ShowWinScreen()
    {
        if(_winPanel != null)
        {
            _winPanel.SetActive(true);
        }
        if(_winMessageText != null)
        {
            _winMessageText.text = "Congratulations, you won!";
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
    }
}
