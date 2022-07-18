using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatusManager : MonoBehaviour
{
    private bool gameOver = false;
    private bool gamePaused = false;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseButton;
    
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
        pauseButton.GetComponent<SpriteRenderer>().enabled = gamePaused;
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
        if (Input.GetKeyDown(KeyCode.Space) && !IsGameOver())
        {
            TogglePause();
        }

        if (IsGameOver() && Input.GetMouseButtonDown(0))
        {
            // Restart the game
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
