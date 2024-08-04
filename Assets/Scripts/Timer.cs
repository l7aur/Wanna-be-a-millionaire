using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 10f;
    [SerializeField] float timeToShowCorrectAnswer = 1.5f;
    float timerValue;

    private bool loadNextQ = true;
    private bool isAnsweringToQ = true;
    private float fillFraction;

    // Start is called before the first frame update
    void Start()
    {
        timerValue = timeToCompleteQuestion;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (isAnsweringToQ)
        {
            if (timerValue > 0)
                fillFraction = timerValue / timeToCompleteQuestion;
            else
            {
                isAnsweringToQ = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue > 0)
                fillFraction = timerValue / timeToShowCorrectAnswer;
            else
            {
                isAnsweringToQ = true;
                loadNextQ = true;
                timerValue = timeToCompleteQuestion;
            }
        }
    }

    public void CancelTimer()
    {
        timerValue = 0f;
    }

    public float GetFillFraction() { return fillFraction; }
    public float GetTimerValue() { return timerValue; }
    public bool GetIsAnsweringToQ() { return isAnsweringToQ; }
    public bool GetLoadNextQ() {  return loadNextQ; }
    public void SetLoadNextQ(bool state) { loadNextQ = state; }
}
