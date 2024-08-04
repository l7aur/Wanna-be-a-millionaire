using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 20f;
    [SerializeField] float timeToShowCorrectAnswer = 1f;
    float timerValue;

    private bool loadNextQ = true;
    private bool isAnsweringToQ = true;
    private float fillFraction;
    private bool stall = true;

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (!stall)
            timerValue -= Time.deltaTime;
        else
            timerValue = timeToCompleteQuestion;
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
    public void setStall(bool state) { stall = state; }
}
