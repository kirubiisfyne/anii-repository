using UnityEngine;
using UnityEngine.UI;

public class StartQuiz : MonoBehaviour
{
    public GameObject quizRoot;
    public Button takeQuizButton;
    public void EnableQuizRoot()
    {
        takeQuizButton.interactable = false;
        quizRoot.SetActive(true);
    }
}
