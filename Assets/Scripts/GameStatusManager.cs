using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatusManager : MonoBehaviour
{
    private bool gameOver = false;
    private bool gamePaused = false;
    [SerializeField] GameObject gameOverScreen;

    
    public void GameOver()
    {
        gameOver = true;
        gamePaused = true;
        Time.timeScale = 0;
        gameOverScreen.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void TogglePause() {
        if (!gameOver)
        {
            gamePaused = !gamePaused;
        }
        Time.timeScale = gamePaused ? 0 : 1;
    }

    public bool GameRunning()
    {
        return !gamePaused && !gameOver;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsGameOver())
            {
                TogglePause();
            }
            else
            {
                // Restart the game
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
