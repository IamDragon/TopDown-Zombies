using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] protected EventSO onPauseToggled;
    [SerializeField] private EventSO gamePausedEvent;
    [SerializeField] private EventSO gameResumedEvent;

    private static bool gamePaused;
    public static bool GamePaused { get { return gamePaused; } }

    private void OnEnable()
    {
        onPauseToggled.Action += TogglePause;
    }

    private void OnDisable()
    {
        onPauseToggled.Action -= TogglePause;
    }

    /// <summary>
    /// Called when player wished to pause/ unpause game
    /// either by cklicking resume or escape
    /// </summary>
    public void TogglePause()
    {
        Debug.Log("pause toggled");
        if(gamePaused)
            ResumeGame();
        else
            PauseGame();
    }


    /// <summary>
    /// Pauses game and fire of game paused event
    /// </summary>
    private void PauseGame()
    {
        Debug.Log("game paused");
        gamePaused = true;
        gamePausedEvent.Invoke();
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resumes game and fires of game resumed event
    /// </summary>
    private void ResumeGame()
    {
        Debug.Log("game resumed");
        gamePaused = false;
        gameResumedEvent.Invoke();
        Time.timeScale = 1;
    }

}
