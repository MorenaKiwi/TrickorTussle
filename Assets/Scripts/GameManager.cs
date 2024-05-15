using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private int score;
    private float gameTimer = 0.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI Luck;
    public GameObject timerParent; // Reference to the parent object that holds the timer elements
    public TextMeshProUGUI timerText; // Reference to the UI Text element
    public Button restartButton;
    public bool isGameActive;

    void Start()
    {
        StartCoroutine(GoodLuck(5f));
        restartButton = GetComponent<Button>();
        restartButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGameActive)
        {
            // Update the timer every frame while the game is active
            gameTimer += Time.deltaTime;
            UpdateTimerUI();
        }
    }

IEnumerator GoodLuck(float displayTime)
{
    yield return new WaitForSeconds(displayTime);
    Luck.gameObject.SetActive(true);
    yield return new WaitForSeconds(displayTime);
    Luck.gameObject.SetActive(false);
    
    // Move the StartGame method outside of the coroutine to start the game immediately
    StartGame();
}


    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        gameTimer = 0.0f; // Reset the timer when the game starts
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {

        Debug.Log("Game Over! Score: " + score + " Time: " + gameTimer.ToString("F2"));
        GameData.GameTime = gameTimer;

        SceneManager.LoadScene("Dead");

        isGameActive = false;
    }

    void UpdateTimerUI()
    {
        // Update the UI Text with the remaining time
        if (timerText != null)
        {
            timerText.text = "Time: " + gameTimer.ToString("F2");
        }
    }

   
}
