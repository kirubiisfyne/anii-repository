using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz")]
public class QuizData : ScriptableObject
{
    public List<QuizItem> quizItems;
}
