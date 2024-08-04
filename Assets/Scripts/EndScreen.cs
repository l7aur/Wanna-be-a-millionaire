using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    private void Update() //check with awake, one call should be enough not each frame
    {
        ShowFinalScore();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "GG!\n" + scoreKeeper.GetScore() + "%";
    }
}
