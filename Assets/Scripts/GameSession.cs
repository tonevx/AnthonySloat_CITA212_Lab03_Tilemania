using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    float loadDelay = 3.0f;
    
    int score = 0;
    int nextLifeThreshold = 1500; 
    const int PointsPerExtraLife = 1500; 

    void Awake()
    {
        int numberGameSessions = FindObjectsByType<GameSession>(FindObjectsInactive.Include).Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        livesText.text = $"Lives: {playerLives}";
        scoreText.text = $"Score: {score}";
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = $"Score: {score}";
        while (score >= nextLifeThreshold)
        {
            playerLives += 1;
            livesText.text = $"Lives: {playerLives}";
            nextLifeThreshold += PointsPerExtraLife; 
        }
    }

    IEnumerator TakeLife()
    {
        playerLives --;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = $"Lives: {playerLives}";
        scoreText.text = $"Score: {score}";
    }
    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        scoreText.text = "Score: 0";
    }
}
