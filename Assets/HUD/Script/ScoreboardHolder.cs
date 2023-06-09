using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI killsText;
    [SerializeField] private TextMeshProUGUI headshotsText;
    [SerializeField] private TextMeshProUGUI downssText;

    public void UpdateStats(PlayerStats stats)
    {
        pointsText.text = stats.points.ToString();
        killsText.text = stats.kills.ToString();
        headshotsText.text = stats.headshots.ToString();
        downssText.text = stats.downs.ToString();
    }
}
