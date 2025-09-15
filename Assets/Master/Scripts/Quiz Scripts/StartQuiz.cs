using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartQuiz : MonoBehaviour
{
    public GameObject quizRoot;
    public Animation kuboAnimation;
    public bool canTakeQuiz;

    private void OnEnable()
    {
        PointsEXPSystem.Instance.OnQuizEnabled += UnlockQuiz;
    }

    private void OnDisable()
    {
        PointsEXPSystem.Instance.OnQuizEnabled -= UnlockQuiz;
    }

    private void UnlockQuiz()
    {
        canTakeQuiz = true;
        kuboAnimation.Play();
    }
    public void EnableQuizRoot()
    {
        if (canTakeQuiz)
        {
            canTakeQuiz = false;
            quizRoot.SetActive(true);
        }
        else
        {
            GameObject floatingText = Instantiate(GameManager.Instance.floatingText, GameManager.Instance.canvas.transform);
            floatingText.GetComponent<TMP_Text>().text = "Not Enough Coins!";
        }
    }
}
