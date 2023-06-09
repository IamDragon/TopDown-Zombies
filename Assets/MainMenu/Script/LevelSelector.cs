using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    /*
     * main menu = scene 0
     * test = scene 1
     * Kino = scene 2
     */


    public void StartKino()
    {
        SceneManager.LoadScene(2);
    }
}
