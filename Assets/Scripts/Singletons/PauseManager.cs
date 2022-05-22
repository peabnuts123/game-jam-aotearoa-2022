using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Public references
    [NotNull(IgnorePrefab = true)]
    public GameObject pauseMenu;

    // Private state
    private bool isGamePaused = false;
    private bool isGameOver = false;

    public void SetGameOver(bool isGameOver)
    {
        // Game over implies paused
        this.isGameOver = isGameOver;
        SetGamePaused(isGameOver);
        pauseMenu.SetActive(false);
    }

    public void SetGamePaused(bool isPaused)
    {
        this.isGamePaused = isPaused;
        this.pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isGameOver)
        {
            SetGamePaused(!this.isGamePaused);
        }
    }

    void OnDestroy()
    {
        // If the PauseManager is destroyed (e.g. changing scenes),
        //  unpause the game
        Time.timeScale = 1;
    }

    public bool IsPaused
    {
        get { return this.isGamePaused; }
    }
}