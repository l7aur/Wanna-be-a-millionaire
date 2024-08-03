using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(1, 6)] //number of lines of text displayed in unity
    [SerializeField] private string question = "Enter question text here.";

    [SerializeField] private List<string> answers = new(4);
    private readonly List<string> scrambledAnswers;
    [SerializeField] private string correctAnswer;

    public void ShuffleAnswers()
    {
        System.Random random = new System.Random();
        List<string> scrambledAnswers = answers.OrderBy(x => random.Next()).ToList();
        answers = scrambledAnswers;
    }

    public string GetQuestion() { return question; }
    public string GetCorrectAnswer() { return correctAnswer; }
    public string GetAnswerIndex(int answerIndex)
    {
        try
        {
            return answers[answerIndex];
        }
        catch(IndexOutOfRangeException e)
        {
            Debug.Log("Index out of bounds!\n" + e.Message);
            return null;
        }
    }
}
