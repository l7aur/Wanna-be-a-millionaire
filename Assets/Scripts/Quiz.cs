using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite wrongAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    // Start is called before the first frame update
    void Start()
    {
        question.ShuffleAnswers();
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons.ElementAt(i).GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswerIndex(i);
        }
    }

    public void OnAnswerSelected(int answerID)
    {
        Image correctButtonImage = answerButtons[question.GetCorrectAnswerIndex()].GetComponent<Image>();
        correctButtonImage.sprite = correctAnswerSprite;

        if (question.GetAnswerIndex(answerID).Equals(question.GetCorrectAnswer()))
            questionText.text = "Correct!";
        else
        {
            questionText.text = "Wrong!";
            Image chosenButton = answerButtons[answerID].GetComponent<Image>();
            chosenButton.sprite = wrongAnswerSprite;
        }
    }
}
