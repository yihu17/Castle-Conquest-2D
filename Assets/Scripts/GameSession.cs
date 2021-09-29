using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;

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

    public void PlayerDeath()
    {
        Debug.Log("Processing...");
        if (playerLives > 1)
        {
            playerLives--;
            Debug.Log("Lives Left: " + playerLives);
        }
        else
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
