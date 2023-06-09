using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScoreboard : MonoBehaviour
{
    [SerializeField] private PlayerStatsEventSO updateHudEvent;
    //[SerializeField] private EventSO hideScoreBoardEvent;


    [SerializeField] private ScoreboardHolder holder;
    private bool active;
    public static HUDScoreboard Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        HideScoreboard();
    }

    private void OnEnable()
    {
        updateHudEvent.Action += UpdateScore;
        //UpdateScoreboard += ShowScoreBoard;
        //hideScoreBoardEvent.Action += HideScoreboard;
    }

    private void OnDisable()
    {
        updateHudEvent.Action -= UpdateScore;
        //UpdateScoreboard -= ShowScoreBoard;
        //hideScoreBoardEvent.Action -= HideScoreboard;
    }

    private void UpdateScore(PlayerStats stats)
    {
        holder.UpdateStats(stats);
    }

    private void ShowScoreBoard(PlayerStats stats)
    {
        UpdateScore(stats);
        this.gameObject.SetActive(true);
        active= true;
    }

    private void HideScoreboard()
    {
        this.gameObject.SetActive(false);
        active= false;
    }

    public void ShowHideScoreboard(PlayerStats stats)
    {
        if(active)
            HideScoreboard();
        else
            ShowScoreBoard(stats);
    }
}
