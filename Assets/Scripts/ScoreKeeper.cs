using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int correctAnswers = 0;
    private int questionsSeen = 0;

    public int GetScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }

    public int GetCorrectAnswers() {  return correctAnswers; }
    public void IncrementCorrectAnswers() { correctAnswers++; }
    public int GetQuestionsSeen() { return questionsSeen; }
    public void IncrementQuestionsSeen() { questionsSeen++; }
}
