using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "Question")]
public class QuestionSO : ScriptableObject
{
    private const int n = 4;
    private List<string> scrambledAnswers;

    [TextArea(1, 6)] //number of lines of text displayed in unity
    [SerializeField] private string question = "Enter question text here.";
    [SerializeField] private List<string> answers = new(n);
    [SerializeField] private string correctAnswer;

    public void ShuffleAnswers()
    {
        System.Random random = new();
        scrambledAnswers = answers.OrderBy(x => random.Next()).ToList();
    }

    public string GetQuestion() { return question; }
    public string GetCorrectAnswer() { return correctAnswer; }

    public int GetCorrectAnswerIndex()
    {
        for(int i = 0; i < n; i++)
            if(scrambledAnswers.ElementAt(i).Equals(correctAnswer))
                return i;
        return -1;
    }

    public string GetAnswerIndex(int answerIndex)
    {
        try
        {
            return scrambledAnswers.ElementAt(answerIndex);
        }
        catch(IndexOutOfRangeException e)
        {
            Debug.Log("Index out of bounds!\n" + e.Message);
            return null;
        }
    }
}
