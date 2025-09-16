using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsEXPSystem : MonoBehaviour
{
    public static PointsEXPSystem Instance;

    [Header("UI References")]
    public TMP_Text pointsTMP;
    public Slider EXPTMP;

    public TMP_Text pointsThresholdTMP;
    public TMP_Text EXPTresholdTMP;

    public TMP_Text LevelTMP;

    //Quiz Settings
    [Header("Quiz Settings")]
    private int _currentPoints = 20;

    public int pointThreshold = 50;
    public int nextQuizIndex = 0;

    //EXP Settings
    [Header("EXP Settings")]
    public int _currentLevel = 0;
    public int _currentEXP = 0;

    public int EXPThreshold = 50;
    public bool[] cropKeys = { true, true, false, false, false, false };

    public event Action<int> OnLevelUp;
    public event Action OnQuizEnabled;

    private String[] experienceRanks = { "Novice", "Experienced", "Expert" };

    public int CurrentPoints
    {
        get => _currentPoints;
        set
        {
            if (_currentPoints != value)
            {
                //Execute everytime the CurrentPoints value is changed.
                _currentPoints = value;
                pointsTMP.text = _currentPoints.ToString();
                if (_currentPoints >= pointThreshold)
                {
                    OnQuizEnabled?.Invoke();
                }
            }
        }
    }

    public int CurrentLevel
    {
        get => _currentLevel;
        private set
        {
            if (_currentLevel != value)
            {
                // Execute everytime the CurrentLevel value is changed.
                _currentLevel = value;
                LevelTMP.text = experienceRanks[value];
                EXPTMP.maxValue = EXPThreshold;
            }
        }
    }

    public int CurrentEXP
    {
        get => _currentEXP;
        set
        {
            if (_currentEXP != value)
            {
                // Execute everytime the CurrentEXP value is changed.
                _currentEXP = value;
                EXPTMP.value = _currentEXP;
                LevelHandler();
            }
        }
    }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }
        Instance = this;
    }

    public void LevelHandler()
    {
        while (CurrentEXP >= EXPThreshold)
        {
            // Execute if CurrentEXP is equal or greater than the EXPThreshold.
            CurrentLevel++;
            CurrentEXP -= EXPThreshold;

            EXPThreshold = (int)(EXPThreshold * 1.5f);
            pointThreshold = (int)(pointThreshold * 1.5f);

            EXPTresholdTMP.text = "/ " + EXPThreshold.ToString();
            pointsThresholdTMP.text = "/ " + pointThreshold.ToString();

            for (int i = 0; i < (CurrentLevel + 1) * 2; i++)
            {
                cropKeys[i] = true;
                Debug.Log(i.ToString() + cropKeys[i].ToString());
            }

            OnLevelUp?.Invoke(_currentLevel);
        }
    }

    public void DecreasePoints(int amount)
    {
        if (CurrentPoints < amount)
        {
            Debug.LogWarning($"Not enough points to decrease by {amount}! Need at least {amount}.");
            return;
        }

        CurrentPoints -= amount;
        if (CurrentPoints < 0) CurrentPoints = 0;

        Debug.Log($"Points decreased by {amount}. Current: {CurrentPoints}");
    }

    // Updated to use AniiEventManager
    public void AddPoints(int amount)
    {
        if (AniiEventManager.Instance != null && AniiEventManager.Instance.IsBonusActive())
        {
            amount = AniiEventManager.Instance.GetBoostedHarvestPoints(amount);
        }
        CurrentPoints += amount;
        Debug.Log($"Added {amount} points. Current: {CurrentPoints}");
    }
}
