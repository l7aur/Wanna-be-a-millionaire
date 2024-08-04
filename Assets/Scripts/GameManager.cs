using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;
    StartScreen startScreen;
    Timer timer;

    // Start is called before the first frame update
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
        startScreen = FindObjectOfType<StartScreen>();
        timer = FindObjectOfType<Timer>();
    }

    void Start()
    {
        startScreen.gameObject.SetActive(true);
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
        }
    }

    public void OnStartGame()
    {
        startScreen.gameObject.SetActive(false);
        quiz.gameObject.SetActive(true);
        timer.setStall(false);
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
