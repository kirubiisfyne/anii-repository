using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public TMP_Text pointsTMP;
    public TMP_Text EXPTMP;

    //Quiz Settings
    [Header("Quiz Settings")]
    private int _currentPoints;

    public int pointThreshold = 50;
    public int nextQuizIndex = 0;

    //EXP Settings
    [Header("EXP Settings")]
    public int _currentLevel = 0;
    public int _currentEXP = 0;

    public int EXPThreshold = 50;
    public bool[] cropKeys = { true, false };

    public event Action<int> OnLevelUp;

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
                //Execute everytime the CurrentLevel value is changed.
                _currentLevel = value;
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
                //Execute everytime the CurrentEXP value is changed.
                _currentEXP = value;
                EXPTMP.text = _currentEXP.ToString();
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
        DontDestroyOnLoad(gameObject);
    }

    public void LevelHandler()
    {
        while (CurrentEXP >= EXPThreshold)
        {
            //Execute if CurrentEXP is equal or greater than the EXPThreshold.
            CurrentLevel++;
            CurrentEXP -= EXPThreshold;
            EXPThreshold = (int)(EXPThreshold * 1.5f);

            for (int i = 0; i < cropKeys.Length; i++)
            {
                cropKeys[i] = true;
                Debug.Log(i.ToString() + cropKeys[i].ToString());
            }

            OnLevelUp?.Invoke(_currentLevel);
        }
    }
}
