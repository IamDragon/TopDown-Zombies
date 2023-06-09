using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/PlayerStatsEvent")]
public class PlayerStatsEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<PlayerStats> Action;

    public void Invoke(PlayerStats stats) => Action?.Invoke(stats);
}
