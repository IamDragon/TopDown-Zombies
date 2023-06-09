using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTextManager : MonoBehaviour
{
    private TextMeshProUGUI textElement;

    [Header("Events")]
    [SerializeField] private IntEventSO roundEndEvent;


    void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        roundEndEvent.Action += UpdateText;
    }

    private void OnDisable()
    {
        roundEndEvent.Action -= UpdateText;
    }

    private void UpdateText(int roundNumber)
    {
        if (textElement == null) return;

        //play animation
        StartCoroutine(RoundChangeAnimation(roundNumber));
    }

    private IEnumerator RoundChangeAnimation(int roundNumber)
    {
        FadeOut();
        yield return new WaitForSeconds(2);
        textElement.text = roundNumber.ToString();
        FadeIn();
    }

    private void FadeIn()
    {
        textElement.CrossFadeAlpha(1.1f, 1, false);
    }

    private void FadeOut()
    {
        textElement.CrossFadeAlpha(0.0f, 1, false);
    }
}
