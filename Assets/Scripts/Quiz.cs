using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    private bool hasAnsweredEarly = false;

    [Header("Button Sprites")]
    [SerializeField] Sprite wrongAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }

    void Update()
    {
        timerImage.fillAmount = timer.GetFillFraction();
        if (timer.GetLoadNextQ())
        {
            GetNextQuestion();
            timer.SetLoadNextQ(false);
        }
        else if(!hasAnsweredEarly && !timer.GetIsAnsweringToQ())
        {
            DisplayAnswer(-1);
            SetButtonInteractability(false);
        }
    }

    private void GetNextQuestion()
    {
        SetButtonInteractability(true);
        ResetButtonSprites();
        DisplayQuestionAndAnswers();
    }

    private void ResetButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image image = answerButtons.ElementAt(i).GetComponent<Image>();
            image.sprite = defaultAnswerSprite; 
        }
    }

    private void DisplayQuestionAndAnswers()
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
        DisplayAnswer(answerID);
        SetButtonInteractability(false);
        timer.CancelTimer();
    }

    private void DisplayAnswer(int answerID)
    {
        Image correctButtonImage = answerButtons[question.GetCorrectAnswerIndex()].GetComponent<Image>();
        correctButtonImage.sprite = correctAnswerSprite;
        if (answerID < 0) 
            return;
        if (question.GetAnswerIndex(answerID).Equals(question.GetCorrectAnswer()))
            questionText.text = "Correct!";
        else
        {
            questionText.text = "Wrong!";
            Image chosenButton = answerButtons[answerID].GetComponent<Image>();
            chosenButton.sprite = wrongAnswerSprite;
        }
    }

    void SetButtonInteractability(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons.ElementAt(i).GetComponent<Button>();
            button.interactable = state;
        }
    }

}
