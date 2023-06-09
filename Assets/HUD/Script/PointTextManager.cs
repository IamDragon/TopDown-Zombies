using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointTextManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private IntEventSO onAddPoints;
    [SerializeField] private IntEventSO onRemovePoints;

    private TextMeshProUGUI textElement;
    private FloatingTextManager textManager;

    private void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
        textManager = GetComponent<FloatingTextManager>();
        SetPointText();
    }

    private void OnEnable()
    {
        onAddPoints.Action += PointsAdded;
        onRemovePoints.Action += PointsRemoved;
    }

    private void OnDisable()
    {
        onAddPoints.Action -= PointsAdded;
        onRemovePoints.Action -= PointsRemoved;
    }

    private void PointsAdded(int amount)
    {
        //play moving number
        SetPointText();
        textManager.Show("+ " + amount.ToString(), Color.yellow);
    }

    private void PointsRemoved(int amount)
    {
        SetPointText();
        textManager.Show("- " + amount.ToString(), Color.yellow);
    }

    private void SetPointText()
    {
        if (Player.Instance != null && textElement != null)
        {
            textElement.text = Player.Instance.PointManager.GetPoints().ToString();
        }
        else
            Debug.LogWarning("pointManager Instance or textElement was null");
    }
}
