using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject confirmQuit;
    [SerializeField] private GameObject levelSelect;
    public void HideMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void HideSettingsMenu()
    {
        settings.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
        settings.SetActive(true);
    }

    public void ShowConfirmQuit()
    {
        confirmQuit.SetActive(true);
    }

    public void HideConfirmQuit()
    {
        confirmQuit?.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLevelSelect()
    {
        levelSelect.SetActive(true);
    }

    public void HideLevelSelect()
    {
        levelSelect?.SetActive(false);
    }
}
