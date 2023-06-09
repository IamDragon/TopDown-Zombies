using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverController : MonoBehaviour
{
    //player true death -> end game -> wait 10s ->load main menu
    [SerializeField] private float timeBeforeSceneExit;
    [Header("Events")]
    [SerializeField] protected EventSO onPlayerTrueDeath;
    [SerializeField] protected EventSO onGameOver;

    private static bool gameOver;
    public static bool GameOver { get { return gameOver; } }

    private void OnEnable()
    {
        onPlayerTrueDeath.Action += EndGame;
    }

    private void OnDisable()
    {
        onPlayerTrueDeath.Action -= EndGame;
    }

    private void EndGame()
    {
        gameOver = true;
        onGameOver.Invoke();
        Invoke(nameof(LoadMainMenu), timeBeforeSceneExit);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
