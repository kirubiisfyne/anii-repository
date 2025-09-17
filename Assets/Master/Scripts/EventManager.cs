using UnityEngine;
using System.Collections;
using TMPro; // Requires TextMeshPro for TMP_Text

public class AniiEventManager : MonoBehaviour
{
    public static AniiEventManager Instance { get; private set; } // Singleton instance for global access

    [Header("Anii Event Settings")]
    public float eventInterval = 5f; // 5 seconds for testing (change back to 5f * 60f for 5 minutes)
    public float activationChance = 0.4f; // 40% chance
    public float boostDuration = 2f * 60f; // 2 minutes in seconds
    public float harvestBoostMultiplier = 1.5f; // +50% boost (1.5x normal)

    [Header("UI References")]
    public GameObject aniiFloatingText; // Assign a TextMeshPro UI element in Inspector

    [Header("References")]
    public PointsEXPSystem pointsSystem; // Assign your PointsEXPSystem in Inspector

    private bool isBonusActive = false; // Tracks if the Anii bonus is currently active
    private float bonusTimer = 0f; // Timer for the bonus duration
    private Coroutine eventCoroutine; // Coroutine for the event loop

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }
        Instance = this; // Set the singleton instance
    }

    void Start()
    {
        Debug.Log("AniiEventManager starting with eventInterval: " + eventInterval + " seconds");
        eventCoroutine = StartCoroutine(EventLoop()); // Start the event loop coroutine
        if (pointsSystem == null)
        {
            pointsSystem = PointsEXPSystem.Instance; // Auto-find if not assigned
        }
        if (aniiFloatingText != null)
        {
            aniiFloatingText.gameObject.SetActive(false); // Hide initially
            Debug.Log("aniiFloatingText assigned and initially hidden.");
        }
        else
        {
            Debug.LogWarning("aniiFloatingText is not assigned in the Inspector!");
        }
    }

    private IEnumerator EventLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(eventInterval); // Wait for the event interval
            Debug.Log("Checking for Anii event activation...");
            RollForActivation();
        }
    }

    private void RollForActivation()
    {
        float roll = Random.Range(0f, 1f); // Generate a random value between 0 and 1
        Debug.Log("Rolled value: " + roll + ", Activation threshold: " + activationChance);
        if (roll <= activationChance)
        {
            ActivateBonus();
            Debug.Log("Anii Event Activated! Harvest points boosted for 2 minutes.");
        }
        else
        {
            Debug.Log("Anii Event Rolled - No activation this time.");
        }
    }

    private void ActivateBonus()
    {
        isBonusActive = true; // Activate the bonus
        bonusTimer = boostDuration; // Set the bonus timer
        if (aniiFloatingText != null)
        {
            GameObject floatingText = Instantiate(aniiFloatingText, GameManager.instance.canvas);
            floatingText.GetComponentInChildren<TMP_Text>().text = "Anii Bonus Active! +50% Harvest (2:00)"; // Set initial message
            Debug.Log("aniiFloatingText activated with text: " + floatingText.GetComponentInChildren<TMP_Text>().text);
        }
        else
        {
            Debug.LogWarning("aniiFloatingText is null, cannot activate!");
        }
        // Optional: Add sound or particles here
    }

    void Update()
    {
        if (isBonusActive)
        {
            bonusTimer -= Time.deltaTime; // Decrease the bonus timer
            if (aniiFloatingText != null && bonusTimer > 0)
            {
                int minutes = Mathf.FloorToInt(bonusTimer / 60f); // Calculate minutes remaining
                int seconds = Mathf.FloorToInt(bonusTimer % 60f); // Calculate seconds remaining
                StartCoroutine(StartAniiCountdown(minutes, seconds)); // Update countdown display

            }
            if (bonusTimer <= 0f)
            {
                DeactivateBonus();
            }
        }
    }

    private IEnumerator StartAniiCountdown(int minutes, int seconds)
    {

        yield return new WaitForSecondsRealtime(1);
        GameObject floatingText = Instantiate(aniiFloatingText, GameManager.Instance.canvas.transform);
        floatingText.GetComponentInChildren<TMP_Text>().text = "Anii Bonus Active! +50% Harvest (2:00)"; // Set initial message
        floatingText.GetComponentInChildren<TMP_Text>().text = $"Anii Bonus Active! +50% Harvest ({minutes:00}:{seconds:00})"; // Update countdown
    }


    private void DeactivateBonus()
    {
        isBonusActive = false; // Deactivate the bonus
        if (aniiFloatingText != null)
        {
            aniiFloatingText.gameObject.SetActive(false); // Hide the indicator
            Debug.Log("aniiFloatingText deactivated.");
        }
        Debug.Log("Anii Bonus Expired.");
    }

    public bool IsBonusActive()
    {
        return isBonusActive; // Check if the bonus is active
    }

    public int GetBoostedHarvestPoints(int basePoints)
    {
        int finalPoints = basePoints; // Initialize final points
        bool harvestedDuringBonus = false; // Track if harvest occurred during bonus

        if (isBonusActive)
        {
            finalPoints = (int)(basePoints * harvestBoostMultiplier); // Apply the boost
            harvestedDuringBonus = true;
            Debug.Log($"Anii Boost Applied! Base: {basePoints}, Boosted: {finalPoints}");
        }

        if (harvestedDuringBonus && pointsSystem != null)
        {
            pointsSystem.CurrentEXP += basePoints; // Award EXP based on base points
            Debug.Log($"Anii Bonus Harvest! Gained {basePoints} EXP.");
        }

        return finalPoints; // Return the final points value
    }
}