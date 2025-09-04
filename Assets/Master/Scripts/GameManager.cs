using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    public static GameManager Instance;

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
}
