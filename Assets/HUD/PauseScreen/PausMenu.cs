using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausMenu : MonoBehaviour
{
    [SerializeField] private Transform pausMenuTransfrom;
    private bool pauseMenuOpen;
    [SerializeField] private EventSO gamePausedEvent;
    [SerializeField] private EventSO gameResumedEvent;
    [SerializeField] private EventSO gameQuitEvent;
    private void OnEnable()
    {
        gamePausedEvent.Action += ShowPauseMenu;
        gameResumedEvent.Action += ResumeGame;
        gameQuitEvent.Action += QuitToMainMenu;
    }

    private void OnDisable()
    {
        gamePausedEvent.Action -= ShowPauseMenu;
        gameResumedEvent.Action -= ResumeGame;
        gameQuitEvent.Action -= QuitToMainMenu;
    }

    private void ShowPauseMenu()
    {
        Debug.Log("Show pause menu");
        pausMenuTransfrom.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("Hide pause menu");
        pausMenuTransfrom.gameObject.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        Debug.Log("Quit to main menu - not yet implemented");
        //Application.Quit();
    }
}
