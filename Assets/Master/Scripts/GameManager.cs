using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text pointsTMP;

    //Quiz Settings
    public int quizPointRequiment = 50;
    public int nextQuizIndex = 0;

    private int _currentPoints;
    public int CurrentPoints
    {
        get => _currentPoints;
        set
        {
            if (_currentPoints != value)
            {
                _currentPoints = value;
                pointsTMP.text = _currentPoints.ToString();
            }
        }
    }

    public void Awake()
    {
        Instance = this;
    }
}
