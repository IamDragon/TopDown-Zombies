using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHideOnGameOver : MonoBehaviour
{
    [SerializeField] private EventSO onGameOver;
    [SerializeField] private Transform[] HUDToHide;

    private void OnEnable()
    {
        onGameOver.Action += HideThings;
    }

    private void OnDisable()
    {
        onGameOver.Action -= HideThings;
    }

    private void HideThings()
    {
        foreach (Transform t in HUDToHide)
        {
            t.gameObject.SetActive(false);
        }
    }
}
