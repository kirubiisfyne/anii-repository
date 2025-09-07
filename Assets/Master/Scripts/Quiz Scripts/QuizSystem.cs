using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Quizzes")]
    public List<QuizData> quizzes = new List<QuizData> { };
    public QuizData activeQuiz;
    public QuizItem activeQuizItem;

    public Queue<QuizItem> enqueuedQuizItems = new Queue<QuizItem> { };

    public int EXPGain = 50;

    [Header("UI")]
    public Image quizRoot;
    public TMP_Text questionTMP;
    public TMP_Text blank; // Where the player-input syllables goes.

    public HorizontalLayoutGroup buttonContainer;
    public GameObject buttonPrefab;

    public Button takeQuizButton;
    private List<GameObject> buttons = new List<GameObject> { };

    private void OnEnable()
    {
        activeQuiz = quizzes[PointsEXPSystem.Instance.nextQuizIndex];

        activeQuiz.quizItems = activeQuiz.quizItems.OrderBy(x => Random.value).ToList();
        for (int i = 0; i < 5; i++)
        {
            enqueuedQuizItems.Enqueue(activeQuiz.quizItems[i]);
        }

        ResetQuizItem();
    }

    private void ResetQuizItem()
    {
        Debug.Log("New Item!");
        if (enqueuedQuizItems.Count > 0)
        {
            if (buttons.Count() > 0)
            {
                foreach (GameObject button in buttons)
                {
                    Destroy(button);
                }
            }

            buttons.Clear();
            blank.text = string.Empty;

            activeQuizItem = enqueuedQuizItems.Dequeue();
            List<string> syllableList = activeQuizItem.answer.Split("-").OrderBy(x => Random.value).ToList();


            questionTMP.text = activeQuizItem.question;
            foreach (string syllable in syllableList)
            {
                GameObject tempButton = Instantiate(buttonPrefab, buttonContainer.transform);
                tempButton.GetComponentInChildren<TMP_Text>().text = syllable;
                tempButton.GetComponent<Button>().onClick.AddListener(() => OnButtonPressed(tempButton.GetComponent<Button>()));

                buttons.Add(tempButton);
            }
        }

        else
        {
            Debug.Log("No more quiz items in the quiz!");

            blank.text = string.Empty;

            PointsEXPSystem.Instance.CurrentPoints -= PointsEXPSystem.Instance.EXPThreshold;
            PointsEXPSystem.Instance.CurrentEXP += EXPGain;
            
            PointsEXPSystem.Instance.nextQuizIndex++;
            Debug.Log("Point Threshold: " + PointsEXPSystem.Instance.pointThreshold.ToString());

            quizRoot.gameObject.SetActive(false);
        }
    }

    public void OnButtonPressed(Button button)
    {
        blank.text += button.GetComponentInChildren<TMP_Text>().text;
        Debug.Log("Blank Text: " + blank.text);
        Debug.Log("Blank Text Length: " + blank.text.Length);

        button.interactable = false;

        string correctWord = string.Join("", activeQuizItem.answer.Split("-"));
        Debug.Log("Correct Word: " + correctWord);
        Debug.Log("Correct Word Length: " + correctWord.Length);

        if (blank.text.Length == correctWord.Length)
        {
            if (blank.text == correctWord)
            {
                Debug.Log("Correct!");
                ResetQuizItem();
                ReenableAllButtons();
            }
            else
            {
                Debug.Log("Wrong!");
                if (EXPGain > 0)
                {
                    EXPGain -= 10;
                }

                blank.text = string.Empty;
                ReenableAllButtons();
            }
        }
    }
    private void ReenableAllButtons()
    {
        foreach (Button button in buttonContainer.GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
    }
}
