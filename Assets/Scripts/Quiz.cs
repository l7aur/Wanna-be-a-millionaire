using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new();
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

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    private bool isComplete = false;

    // Start is called before the first frame update
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        progressBar.maxValue = (questions.Count < 10) ? questions.Count : 10;
        progressBar.value = 0;
    }

    void Update()
    {
        timerImage.fillAmount = timer.GetFillFraction();
        if (timer.GetLoadNextQ())
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            GetNextQuestion();
            timer.SetLoadNextQ(false);
        }
        else if (!hasAnsweredEarly && !timer.GetIsAnsweringToQ())
        {
            DisplayAnswer(-1);
            SetButtonInteractability(false);
        }
    }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonInteractability(true);
            ResetButtonSprites();
            GetRandomQuestion();
            DisplayQuestionAndAnswers();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
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

    private void ResetButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image image = answerButtons.ElementAt(i).GetComponent<Image>();
            image.sprite = defaultAnswerSprite;
        }
    }

    private void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions.ElementAt(index);
        if (questions.Contains(currentQuestion))
            questions.Remove(currentQuestion);

    }

    private void DisplayQuestionAndAnswers()
    {
        currentQuestion.ShuffleAnswers();
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons.ElementAt(i).GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswerIndex(i);
        }
    }

    public void OnAnswerSelected(int answerID)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(answerID);
        SetButtonInteractability(false);
        timer.CancelTimer();
        scoreText.text = scoreKeeper.GetCorrectAnswers().ToString() + "/" + scoreKeeper.GetQuestionsSeen().ToString();
    }

    private void DisplayAnswer(int answerID)
    {
        Image correctButtonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
        correctButtonImage.sprite = correctAnswerSprite;
        if (answerID < 0 || answerID >= questions.Count) 
            return;
        if (currentQuestion.GetAnswerIndex(answerID).Equals(currentQuestion.GetCorrectAnswer()))
        {
            questionText.text = "Correct!";
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "Wrong!";
            Image chosenButton = answerButtons[answerID].GetComponent<Image>();
            chosenButton.sprite = wrongAnswerSprite;
        }
    }

    public bool GetIsComplete() { return isComplete; }
}
