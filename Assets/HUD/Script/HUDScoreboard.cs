using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScoreboard : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private PlayerStatsEventSO onToggleScoreboardEvent;
    [SerializeField] private EventSO onPauseEvent;
    [SerializeField] private EventSO onGameOverEvent;

    [SerializeField] private Transform scoreboard;
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
        onToggleScoreboardEvent.Action += UpdateScore;
        onPauseEvent.Action += OnPuase;
        onGameOverEvent.Action += ShowScoreBoard;
    }

    private void OnDisable()
    {
        onToggleScoreboardEvent.Action -= UpdateScore;
        onPauseEvent.Action -= OnPuase;
        onGameOverEvent.Action -= ShowScoreBoard;
    }


    private void OnPuase()
    {
        if (PauseManager.GamePaused)
            HideScoreboard();
        else
        {
            if (active)
                scoreboard.gameObject.SetActive(true);
        }

    }

    private void UpdateScore(PlayerStats stats)
    {
        holder.UpdateStats(stats);
    }

    private void ShowScoreBoard(PlayerStats stats)
    {
        UpdateScore(stats);
        scoreboard.gameObject.SetActive(true);
        active = true;
    }

    private void ShowScoreBoard()
    {
        scoreboard.gameObject.SetActive(true);
        active = true;
    }


    private void HideScoreboard()
    {
        scoreboard.gameObject.SetActive(false);
        active = false;
    }

    public void ToggleScoreboard(PlayerStats stats)
    {
        if (active)
            HideScoreboard();
        else
            ShowScoreBoard(stats);
    }
}
