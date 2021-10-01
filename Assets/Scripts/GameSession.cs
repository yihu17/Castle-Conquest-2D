using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3, score = 0;
    [SerializeField] Text scoreText, liveText;
    [SerializeField] Image[] hearts;

    private void Awake()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = score.ToString();
        liveText.text = playerLives.ToString();
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void AddLives()
    {
        if(playerLives >= 3)
        {
            playerLives = 3;
        }
        else
        {
            playerLives++;
            UpdateHearts();
        }

        liveText.text = playerLives.ToString();
    }

    public void PlayerDeath()
    {
        if (playerLives > 1)
        {
            playerLives--;
            UpdateHearts();
            liveText.text = playerLives.ToString();
        }
        else
        {
            ResetGame();
        }
    }

    void UpdateHearts()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < playerLives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
