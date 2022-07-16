using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private bool gameOver = false;
    private bool gamePaused = false;

    private void endGame()
    {
        gameOver = true;
        gamePaused = true;
        Time.timeScale = 0;
    }

    private void togglePause() {
        if (!gameOver)
        {
            gamePaused = !gamePaused;
        }
        Time.timeScale = gamePaused ? 0 : 1;
    }

    private bool gameRunning()
    {
        return !gamePaused && !gameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            togglePause();
        }
    }
}
