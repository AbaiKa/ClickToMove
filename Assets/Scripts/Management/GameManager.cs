using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Properties
    public static GameManager Instance { get; private set; }

    public UnityEvent<int> onCoinsValueChanged { get; private set; } = new UnityEvent<int>();
    public UnityEvent<int> onTimerValueChanged { get; private set; } = new UnityEvent<int>();

    /// <summary>
    /// True if player wins
    /// </summary>
    public UnityEvent<bool> onGameOver { get; private set; } = new UnityEvent<bool>();

    public bool gameIsOver { get; private set; } = false;

    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private int _startTimer = 15;
    [SerializeField] private int _coinsToWin = 10;
    [SerializeField]
    [Tooltip("Дополнительное время после того как получили монетку")]
    private int _addTime = 5;

    private int _timer;
    private int _coins;
    #endregion

    #region Methods

    #region Unity
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetActiveGameOverPanel(false); 

        _timer = _startTimer;
        _coins = 0;

        UpdateCoins();
        InvokeRepeating(nameof(UpdateTimer), 1, 1);
    }
    #endregion

    #region Public
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddCoins()
    {
        _coins++;
        _timer = Mathf.Min(_startTimer, _timer + _addTime);
        UpdateCoins();
    }
    #endregion

    #region Private
    private void UpdateTimer()
    {
        _timer--;

        if (_timer <= 0)
        {
            SetActiveGameOverPanel(true);
            onGameOver?.Invoke(false); // lose
            DeInit();
            return;
        }

        int res = Mathf.Min(_startTimer, _timer);
        onTimerValueChanged?.Invoke(res);
    }

    private void UpdateCoins()
    {
        onTimerValueChanged?.Invoke(_timer);
        onCoinsValueChanged?.Invoke(_coins);

        if (_coins >= _coinsToWin)
        {
            SetActiveGameOverPanel(true);
            onGameOver?.Invoke(true); // win
            DeInit();
        }
    }

    private void SetActiveGameOverPanel(bool value)
    {
        _mainMenuPanel.SetActive(!value);
        _gameOverPanel.SetActive(value);
        gameIsOver = value;
    }

    private void DeInit()
    {
        CancelInvoke();
    }
    #endregion

    #endregion
}
